using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunabank.Data.Models;
using Lunabank.Data.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(ILogger<AdminController> logger, IAdminRepo accountActivities, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _accountActivities = accountActivities;
            _roleManager = roleManager;
            _userManager = userManager;
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

        [HttpPost("createRole")]
        public async Task<ActionResult> CreatRole(Role role)
        {

            

            if (ModelState.IsValid)
            {
                var res = await _roleManager.RoleExistsAsync(role.Roles);

                if (res == true)
                {
                    return BadRequest("Role already available");
                }

                IdentityRole identityRole = new IdentityRole
                {
                    Name = role.Roles
                };

                await  _roleManager.CreateAsync(identityRole);
                

                
                return Ok("Successful");
            }

            return BadRequest("Error");
        }

        [HttpPost("addRole")]
        public async Task<ActionResult> AddRole(AddRole addRole)
        {
            if (ModelState.IsValid)
            {
              

                var currentUser = await _accountActivities.GetUser(addRole.Email);
              var res =  await _userManager.IsInRoleAsync(currentUser, addRole.Role);

                if (res == true)
                {
                    return BadRequest("User already available");
                }

                await _userManager.AddToRoleAsync(currentUser, addRole.Role);
             



                return Ok("Successful");
            }

            return BadRequest("Error");
        }


    }
}