using System;
using System.Collections.Generic;
using System.Text;

namespace Lunabank.Data.Repos
{
    public interface IUserRepo
    {
        bool EmailExist(string email);
        string GetCurrentUserId();
    }
}
