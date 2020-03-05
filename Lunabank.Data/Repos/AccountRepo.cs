using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lunabank.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lunabank.Data.Repos
{
    public class AccountRepo : IAccounRepo
    {
        private readonly DataContext _context;


        public AccountRepo(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
        }
        public async Task<IEnumerable<Accounts>> GetAllAccount()
        {
            return await _context.Accounts.Include(r=> r.User).ToListAsync();
        }

        public async Task<Accounts> GetAccounts(Guid id)
        {

            return await _context.Accounts.Include(r => r.User).FirstOrDefaultAsync(r => r.AccountId == id);

        }
     
        public async Task<Accounts> Create(Accounts accounts)
        {

            var user = await  _context.Users.FindAsync(accounts.UserId);
            accounts.User = user;
           _context.Add(accounts);
            var success =await _context.SaveChangesAsync() > 0;
            if (success) return accounts;
            return null;
        }
    }
}