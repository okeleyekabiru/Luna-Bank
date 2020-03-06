using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lunabank.Data.Models
{
    public class AccountCreationDto
    {
        [Required]
        public string AccountType { get; set; }
    }
}
