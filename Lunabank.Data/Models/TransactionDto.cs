using System;
using System.Collections.Generic;
using System.Text;

namespace Lunabank.Data.Models
{
    public class TransactionDto
    {
        public DateTime CreatedOn { get; set; }
        public string TransactionType { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal OldBalance { get; set; }
        public decimal NewBalance { get; set; }
    }
}
