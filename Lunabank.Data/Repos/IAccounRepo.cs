using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lunabank.Data.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lunabank.Data.Repos
{
    public interface IAccounRepo
    {
        Task<IEnumerable<Accounts>> GetAllAccount();
        Task<Accounts> GetAccounts(Guid id);
        Task<Accounts> Create(Accounts accounts);

    }
}