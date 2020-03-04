using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lunabank.Data.Entities;
using Lunabank.Data.Models;
using Lunabank.Data.Repos;
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
        private static int PageIndex;

        public AccountController(ILogger<AccountController> logger, IAccounRepo accountRepo, IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _accountRepo = accountRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccount(int pageSize, int pageIndex)
        {
            try
            {
                var model = await _accountRepo.GetAllAccount();
                var totalCount = model.ToList().Count;
                var _pageSize = (pageSize > totalCount) ? totalCount : pageSize;
                PageIndex += pageIndex;


                if (totalCount > 0)
                {
                    var totalPage = model.Skip(_pageSize * (PageIndex - 1)).Take(_pageSize).ToList();
                    var currentUr = _httpContextAccessor.HttpContext.Request.Host.Value +
                                    $"api/account?PageSize={_pageSize}&pageIndex={PageIndex + 1}";
                    var previousUrl = _httpContextAccessor.HttpContext.Request.Host.Value +
                                      $"api/account?PageSize={_pageSize}&pageIndex={PageIndex - 1}";
                    var newModel = _mapper.Map<IEnumerable<Accounts>, IEnumerable<AccountModel>>(totalPage);
                    HttpContext.Response.Headers.Add("NextPage",currentUr);
                    HttpContext.Response.Headers.Add("PreviousPage",previousUrl);
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

        [HttpPost]
        public async Task<IActionResult> Create(Accounts accounts)
        {
            accounts.CreatedOn = DateTime.Now;
            accounts.UserId = "86970b86-9a92-433b-ade0-3e0bb5014ddb";
            accounts.AccountId = Guid.NewGuid();
            var model = await _accountRepo.Create(accounts);
            if (model != null)
            {
                var newmodel = _mapper.Map<Accounts, AccountModel>(model);
                return Ok(newmodel);
            }

            return StatusCode(500, new {error = "Internal Server error"});
        }
    }
}