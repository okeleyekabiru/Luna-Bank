using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lunabank.Data.Models;

namespace Lunabank.Data.Repos
{
    public interface IUserRepo
    {
        bool EmailExist(string email);
        string GetCurrentUserId();
        Task<AppUser> GetLoginUser();
    }
}
