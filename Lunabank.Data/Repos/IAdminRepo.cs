using System;
using System.Collections.Generic;
using System.Text;

namespace Lunabank.Data.Repos
{
    public interface IAdminRepo
    {
        bool DeactivateAccount(Guid accountId);
        bool ActivateAccount(Guid accountId);
        bool DeleteAccount(Guid accountId);
    }
}