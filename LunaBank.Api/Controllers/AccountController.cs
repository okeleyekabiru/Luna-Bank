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

        public AccountController(ILogger<AccountController> logger, IAccounRepo accountRepo,IMapper mapper)
        {
            _logger = logger;
            _accountRepo = accountRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAccount(int pageSize,int PageIndex)
        {
            try
            {
                var model = await _accountRepo.GetAllAccount();
                if (model.ToList().Count > 0)
                {
                    var newmodel = _mapper.Map<IEnumerable<Accounts>, IEnumerable<AccountModel>>(model);
                    return Ok(newmodel);
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                _logger.LogError(error);
                return StatusCode(500, new { Error = "Internal Server Error" });
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