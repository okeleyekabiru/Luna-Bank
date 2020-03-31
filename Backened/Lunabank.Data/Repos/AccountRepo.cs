using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunabank.Data.Entities;
using Lunabank.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lunabank.Data.Repos
{
    public class AccountRepo : IAccounRepo
    {
        private readonly DataContext _context;

        public AccountRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetAllUserAccount(string userId)
        {
            return await _context.Accounts.Where(r => r.UserId.Equals(userId)).Select(r => r.AccountNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Accounts>> GetAllAccount()
        {
            return await _context.Accounts.Include(r => r.User).ToListAsync();
        }

        public async Task<Accounts> GetAccounts(Guid id)
        {
            return await _context.Accounts.Include(r => r.User).FirstOrDefaultAsync(r => r.AccountId == id);
        }

        public async Task<Accounts> GetAccount(string accountNumber)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public void Create(Accounts account)
        {
            account.AccountNumber =  CreateAccountNumber();
            account.AccountId = Guid.NewGuid();
            _context.Accounts.Add(account);
        }

        public string CreateAccountNumber()
        {
            bool isAvailable = true;
            string accountNumber;
            do
            {
                string startWith = "00";
                Random random = new Random();
                string generator = random.Next(0, 99999999).ToString("D8");
                accountNumber = startWith + generator;
                var account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
                if (account == null)
                {
                    isAvailable = false;
                }
            } while (isAvailable);

            return accountNumber;
        }

        public async Task<Accounts> Debit(decimal amount, string accountnumber)
        {

            var account = await _context.Accounts.FirstOrDefaultAsync(r => r.AccountNumber == accountnumber);
            if (account.Balance < amount)
            {
                return null;
            }

            account.Balance -= amount;
            _context.Entry(account).State = EntityState.Modified;
            var success = await _context.SaveChangesAsync() > 0;
            if (success)
            {
                return account;
            }

            return null;
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<Accounts> Credit(decimal amount, string accountnumber)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(r => r.AccountNumber == accountnumber);
            if (account == null)
            {
                return null;
            }
            account.Balance += amount;
            _context.Entry(account).State = EntityState.Modified;
            var success = await _context.SaveChangesAsync() > 0;
            if (success)
            {
                return account;
            }
            return null;

        }
    }
}