﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunabank.Data.Entities;
using Lunabank.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lunabank.Data.Repos
{
    public class AdminRepo : IAdminRepo
    {
        private readonly DataContext _db;

        public AdminRepo(DataContext db)
        {
            _db = db;
        }





        public bool DeactivateAccount(Guid accountId)
        {

            var value = _db.Accounts.FirstOrDefault(x => x.AccountId == accountId);

            if (value == null)
            {
                return false;
            }

            value.Status = "deactivated";
            _db.SaveChanges();
            return true;



        }


        public bool ActivateAccount(Guid accountId)
        {

            var value = _db.Accounts.FirstOrDefault(x => x.AccountId == accountId);

            if (value == null)
            {
                return false;
            }

            value.Status = "activated";
            _db.SaveChanges();
            return true;



        }



        public bool DeleteAccount(Guid accountId)
        {

            var value = _db.Accounts.FirstOrDefault(x => x.AccountId == accountId);

            if (value == null)
            {
                return false;
            }

            _db.Accounts.Remove(value);
            _db.SaveChanges();
            return true;



        }

        public async Task<AppUser> GetUser(string email)
        {

            var response = await _db.Users.FirstOrDefaultAsync(res => res.Email == email);


            return response;

        }


    }
}