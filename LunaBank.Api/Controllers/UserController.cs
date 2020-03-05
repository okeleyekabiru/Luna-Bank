using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Lunabank.Data.Models;
using Lunabank.Data.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LunaBank.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private readonly IUserRepo _userRepo;
        private readonly ApplicationSettings _appSettings;


        #region Constructor

        public UserController(ILogger<UserController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserRepo userRepo, IOptions<ApplicationSettings> appSettings)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepo = userRepo;
            _appSettings = appSettings.Value;
        }



        #endregion



        #region Register

        [HttpPost]
        [Route("Register")]
        //Post : api/user/Register
        public async Task<object> Register(UserDto model)
        {
            if (_userRepo.EmailExist(model.Email))
            {
                return StatusCode(400, "Email already exists");
            }
            var User = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email

            };
            try
            {
                var result = await _userManager.CreateAsync(User, model.Password);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", User.Id.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { result.Succeeded, data = new { token, User.Id, User.FirstName, User.LastName, User.Email } });
            }
            catch (Exception e)
            {
                _logger.LogInformation($"A user was not created {e.StackTrace} on {DateTime.Now}");
                return StatusCode(400, $"{e.Message}");
            }
        }

        #endregion


        #region Login

        [HttpPost]
        [Route("Login")]
        //Post : api/User/Login
        public async Task<IActionResult> Login(UserDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            var status = await _userManager.CheckPasswordAsync(user, model.Password);
            if (user != null && status)
            {
                //                var tokenDescriptor = new SecurityTokenDescriptor
                //                {
                //                    Subject = new ClaimsIdentity(new Claim[]
                //                    {
                //                        new Claim("UserID", user.Id.ToString()),
                //                    }),
                //                    Expires = DateTime.UtcNow.AddMinutes(5),
                //                    SigningCredentials = new SigningCredentials(
                //                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)),
                //                        SecurityAlgorithms.HmacSha256Signature)
                //                };
                //                var tokenHandler = new JwtSecurityTokenHandler();
                //                var securityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                //                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { status, data = new { user.Id, user.FirstName, user.LastName, user.Email } });
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
        }

        #endregion





    }
}