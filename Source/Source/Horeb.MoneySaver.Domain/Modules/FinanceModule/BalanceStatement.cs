using Horeb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.Domain.FinanceModule
{
    public class BalanceStatement: BaseEntity
    {
        public Decimal Opening { get; set; }

        public Decimal Closing { get; set; }

        public int PeriodId { get; set; }

        public int WalletId { get; set; }
    }
}
