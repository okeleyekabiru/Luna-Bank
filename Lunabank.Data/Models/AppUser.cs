using System;
using System.Collections.Generic;
using System.Text;
using Lunabank.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lunabank.Data.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
        
    }
}