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
        public Transaction()
        {
            Category = new TransactionCategory();            
        }

        public decimal Amount { get; set; }
        
        public string Note { get; set; } = String.Empty;

        public int WalletId { get; set; }

        public TransactionCategory Category { get; set; }

        public DateTime UtcOccurredOn { get; set; }
    }
}
