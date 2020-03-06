using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lunabank.Data.Models
{
  public  class DebitModel
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string AccountNumber { get; set; }
    }
}
