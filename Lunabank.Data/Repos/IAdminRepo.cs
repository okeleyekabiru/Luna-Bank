using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lunabank.Data.Models;

namespace Lunabank.Data.Repos
{
    public interface IAdminRepo
    {
        bool DeactivateAccount(Guid accountId);
        bool ActivateAccount(Guid accountId);
        bool DeleteAccount(Guid accountId);
         Task<AppUser> GetUser(string email);
    }
}