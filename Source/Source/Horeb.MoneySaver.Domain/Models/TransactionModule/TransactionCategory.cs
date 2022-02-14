using Horeb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.Domain.TransactionModule
{
    public class TransactionCategory : BaseEntity
    {
        public TransactionCategory(string name) : base() {            
            Name = name;
        }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        [Required]
        public int WalletId { get; set; }
        
        public bool IsIncome { get; set; }
    }
}
