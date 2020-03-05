
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

          
                context.SaveChanges();
            }
        }
    }
