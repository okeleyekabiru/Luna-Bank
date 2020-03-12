using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lunabank.Data.Entities;
using Lunabank.Data.Models;
using Lunabank.Data.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace LunaBank.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAccounRepo _accountRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepo _userRepo;
        private readonly ITransactionRepo _transactionRepo;

        public AccountController(ILogger<AccountController> logger, IAccounRepo accountRepo, IMapper mapper,
            IHttpContextAccessor httpContextAccessor, IUserRepo userRepo, ITransactionRepo transactionRepo)
        {
            _logger = logger;
            _accountRepo = accountRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userRepo = userRepo;
            _transactionRepo = transactionRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccount(int pageSize = 3, int pageIndex = 1)
        {
            try
            {
                var model = await _accountRepo.GetAllAccount();
                var totalCount = model.ToList().Count;
                var _pageSize = (pageSize > totalCount) ? totalCount : pageSize;


                if (totalCount > 0)
                {
                    var totalPage = model.Skip(_pageSize * (pageIndex - 1)).Take(_pageSize).ToList();
                    var currentUr = _httpContextAccessor.HttpContext.Request.Host.Value +
                                    $"api/account?PageSize={_pageSize}&pageIndex={pageIndex + 1}";
                    var previousUrl = _httpContextAccessor.HttpContext.Request.Host.Value +
                                      $"api/account?PageSize={_pageSize}&pageIndex={pageIndex - 1}";
                    var newModel = _mapper.Map<IEnumerable<Accounts>, IEnumerable<AccountModel>>(totalPage);
                    HttpContext.Response.Headers.Add("NextPage", currentUr);
                    HttpContext.Response.Headers.Add("PreviousPage", previousUrl);
                    return Ok(newModel);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                _logger.LogError(error);
                return StatusCode(500, new {Error = "Internal Server Error"});
            }

            return NotFound("No Account registered yet");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAnAccount(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request ");
            }

            try
            {
                var model = await _accountRepo.GetAccounts(id);
                if (model != null)
                {
                    var newModel = _mapper.Map<Accounts, AccountModel>(model);
                    return Ok(newModel);
                }
            }
            catch (Exception e)
            {
                _logger.LogTrace(e.Message);
                return StatusCode(500, new {error = "Internal Server error"});
            }

            return NotFound("Account Not Available");
        }

        #region Create
            
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(AccountCreationDto type)
        {
            try
            {
                var user = await _userRepo.GetLoginUser();
                if (user == null)
                {
                    return NotFound("User not found");
                }

                var account = new Accounts
                {
                    AccountType = type.AccountType,
                    Balance = 0,
                    User = user,
                    CreatedOn = DateTime.Now,
                    Status = "active"
                };
                 _accountRepo.Create(account);
                 var result = await _accountRepo.Save();
                var status = "success";
                var data = new
                {
                    accountNumber = account.AccountNumber,
                    firstName = account.User.FirstName,
                    lastName = account.User.LastName,
                    email = account.User.Email,
                    type = account.AccountType
                };
                return Ok(new { status, data });
            }
            catch (Exception e)
            {
                _logger.LogTrace(e.Message);
                return StatusCode(500, new { error = "Internal Server error" });
            }

        }
        #endregion
        [Authorize]
        [HttpPost()]
        [Route("debitaccount")]
        public async Task<ActionResult> Debit(DebitModel debit)
        {
            try
            {
                var account = await _accountRepo.GetAccount(debit.AccountNumber);
                var oldBalance = account.Balance;
                var debitAccount = await _accountRepo.Debit(debit.Amount, debit.AccountNumber);
                if (debitAccount != null)
                {
                    var transaction = new Transactions
                    {
                        AccountNumber = debit.AccountNumber,
                        Amount = debit.Amount,
                        Cashier = "Decagon",
                        CreatedOn = DateTime.Now,
                        NewBalance = debitAccount.Balance,
                        OldBalance = oldBalance,
                        TransactionType = "debit"
                    };
                    _transactionRepo.create(transaction);
                    await _accountRepo.Save();
                    var data = new
                    {
                        transactionId = transaction.TransactionId,
                        accountNumber = debit.AccountNumber,
                        amount = debit.Amount,
                        accountBalance = debitAccount.Balance,
                        transactionType = "debit"
                    };
                    var status = "success";
                    _logger.LogInformation(
                        $"{debit.Amount} has been debited from your Account {debit.AccountNumber}time {DateTime.Now}");
                    return Ok(new {status, data});
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException?.ToString() ?? e.Message);
                return StatusCode(500, "Internal Server Error");
            }

            return BadRequest("insufficient fund");
        }
        [HttpPost()]
        [Route("creditaccount")]
        public async Task<ActionResult> Credit(CreditModel debit)
        {
            try
            {
                var account = await _accountRepo.GetAccount(debit.AccountNumber);
                var oldBalance = account.Balance;
                var creditAccount = await _accountRepo.Credit(debit.Amount, debit.AccountNumber);
                

                if (creditAccount != null)
                {
                    var transaction = new Transactions
                    {
                        AccountNumber = debit.AccountNumber,
                        Amount = debit.Amount,
                        Cashier = "Decagon",
                        CreatedOn = DateTime.Now,
                        NewBalance = creditAccount.Balance,
                        OldBalance = oldBalance,
                        TransactionType = "credit"
                    };
                    _transactionRepo.create(transaction);
                    await _accountRepo.Save();
                    var data = new
                    {
                        transactionId = transaction.TransactionId,
                        accountNumber = debit.AccountNumber,
                        amount = debit.Amount,
                        accountBalance = creditAccount.Balance,
                        transactionType = "credit"
                    };
                    var status = "success";
                    _logger.LogInformation(
                        $"{debit.Amount} has been Credited from your Account {debit.AccountNumber}time {DateTime.Now}");
                    return Ok(new { status, data });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException?.ToString() ?? e.Message);
                return StatusCode(500, "Internal Server Error");
            }

            return BadRequest("Invalid Account Number");
        }
    }
}