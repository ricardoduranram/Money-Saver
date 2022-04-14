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
        
        public Decimal Amount { get; set; }
        
        public string Description { get; set; }

        public int WalletId { get; set; }

        public int CategoryId { get; set; }

        public DateTime TransactionOn { get; set; }               
    }
}
