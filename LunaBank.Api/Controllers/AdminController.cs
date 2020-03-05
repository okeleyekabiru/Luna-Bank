using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunabank.Data.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LunaBank.Api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminRepo _accountActivities;

        public AdminController(ILogger<AdminController> logger, IAdminRepo accountActivities)
        {
            _logger = logger;
            _accountActivities = accountActivities;
        }

        [HttpGet("activate/{accountId}")]

        public ActionResult Activate(Guid accountId)
        {
            try
            {
                var response = _accountActivities.ActivateAccount(accountId);
                if (response == false)
                {
                    _logger.LogTrace(BadRequest("Invalid Details").ToString());
                    return BadRequest("Invalid Details");
                }

                return Ok("Successful");
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex.Message);
            }
            return Ok();
        }

        [HttpGet("deactivate/{accountId}")]

        public ActionResult Deactivate(Guid accountId)
        {
            try
            {
                var response = _accountActivities.DeactivateAccount(accountId);
                if (response == false)
                {
                    _logger.LogTrace(BadRequest().ToString());
                    return BadRequest("Invalid Details");
                }

                return Ok("Successful");
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex.Message);

            }
            return Ok();
        }

        [HttpGet("delete/{accountId}")]

        public ActionResult Delete(Guid accountId)
        {
            try
            {
                var response = _accountActivities.DeleteAccount(accountId);
                if (response == false)
                {
                    _logger.LogError(BadRequest().ToString());
                    return BadRequest("Invalid Details");
                }

                return Ok("Successful");
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex.Message);
            }
            return Ok();
        }

    }
}