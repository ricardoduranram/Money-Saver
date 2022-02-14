using Horeb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.Domain.WalletModule
{
    public class Wallet : BaseEntity
    {
        public Wallet(string name) :base() {            
            Name = name;            
        }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public Decimal Amount { get; set; }                            
    }
}
