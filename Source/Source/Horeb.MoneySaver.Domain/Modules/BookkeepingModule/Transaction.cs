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
        public decimal Amount { get; set; }
        
        public string Note { get; set; } = String.Empty;

        public int WalletId { get; set; }

        public int CategoryId { get; set; }

        public DateTime UtcOccurredOn { get; set; }
        
        public int MonthlyPeriodId { get; set; }
    }
}
