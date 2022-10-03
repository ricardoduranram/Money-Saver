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
    public class MonthlyBalanceEnquiry: BaseEntity
    {
        public Decimal MonthlyEndingBalance { get; set; }

        public long WalletId { get; set; }

        public long MonthlyPeriodId { get; set; }
    }
}
