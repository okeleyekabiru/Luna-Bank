using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Lunabank.Data.Entities;
using Lunabank.Data.Models;
using Microsoft.AspNetCore.Http;

namespace Lunabank.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepo(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor;
        }
        public bool EmailExist(string email)
        {
            return _context.Users.Any(c => c.Email.Equals(email));
        }

        public string GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User?.Claims?
                .FirstOrDefault(u => u.Type == "UserID").Value;
            return userId;
        }

        public async Task<AppUser> GetLoginUser()
        {
            return await _context.Users.FindAsync(GetCurrentUserId());
        }
    }
}
