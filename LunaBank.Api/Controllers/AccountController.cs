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

        public AccountController(ILogger<AccountController> logger, IAccounRepo accountRepo, IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _accountRepo = accountRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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

           
            var model =await _accountRepo.GetAccounts(id);
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
    }
}