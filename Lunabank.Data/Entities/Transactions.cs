using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunabank.Data.Entities
{
    public partial class Transactions
    {
        [Key]
        public Guid TransactionId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Required]
        [StringLength(50)]
        public string TransactionType { get; set; }
        [Required]
        [StringLength(50)]
        public string AccountNumber { get; set; }
        [Required]
        [Column("cashier")]
        [StringLength(128)]
        public string Cashier { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal OldBalance { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal NewBalance { get; set; }
    }
}
