
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunabank.Data.Entities;
using Microsoft.AspNetCore.Identity;


namespace Lunabank.Data.Models
{
    public class Seed
    {
        public static async Task SeedGenerator(DataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        FirstName = "Bob",
                        UserName = "bob",
                        Email = "bob@gmail.com"
                    }, new AppUser
                    {
                        FirstName = "TIM",
                        UserName = "tim",
                        Email = "tim@gmail.com"
                    }, new AppUser
                    {
                        FirstName = "Dick",
                        UserName = "dick",
                        Email = "dick@gmail.com"
                    },
                };
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Abiola12345%");
                }

            }

            if (!context.Accounts.Any())
            {
                var accounts = new List<Accounts>
                {
                    
                        new Accounts
                        {
                            AccountNumber = "1274747472",
                            AccountId = Guid.NewGuid(),
                            AccountType = "Savings",
                            Balance = 100000,
                            CreatedOn = DateTime.Now,
                            Status = "deactivated",
                            UserId = "64a47b35-0032-47c0-9846-33381ae3aa2f"

                        },

                        new Accounts
                        {
                        AccountNumber = "1453674742",
                        AccountId = Guid.NewGuid(),
                        AccountType = "Current",
                        Balance = 100000,
                        CreatedOn = DateTime.Now,
                        Status = "activated",
                        UserId = "a208e16e-d198-4758-b0cc-22efbb6257fa"

                },

                        new Accounts
                        {
                            AccountNumber = "7753674740",
                            AccountId = Guid.NewGuid(),
                            AccountType = "Current",
                            Balance = 100000,
                            CreatedOn = DateTime.Now,
                            Status = "activated",
                            UserId = "d3c9e581-fb02-4df1-97f5-5d5c71e7b074"

                        }


                };
                context.Accounts.AddRange(accounts);
                context.SaveChanges();
            }

            
        }
        }
    }
