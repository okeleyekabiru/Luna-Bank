using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lunabank.Data.Entities;
using Lunabank.Data.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lunabank.Data.Repos
{
    public interface IAccounRepo
    {
        Task<IEnumerable<Accounts>> GetAllAccount();
        Task<Accounts> GetAccounts(Guid id);
        Task<Accounts> GetAccount(string accountNumber);
        void Create(Accounts account);
        Task<Accounts> Debit(decimal amount, string accountnumber);
        Task<bool> Save();

        Task<Accounts> Credit(decimal amount, string accountnumber);
    }
}