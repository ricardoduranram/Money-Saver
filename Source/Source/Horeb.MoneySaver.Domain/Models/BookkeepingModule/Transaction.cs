using Horeb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.Domain.TransactionModule
{
    public class Transaction : BaseEntity
    {
        public Transaction() :base() {
            Description = String.Empty;
        }
        
        [Required]        
        public Decimal Amount { get; set; }
        
        public string Description { get; set; }

        [Required]
        public long WalletId { get; set; }

        [Required]
        public long CategoryId { get; set; }

        [Required]        
        public DateTime TransactionOn { get; set; }               
    }
}
