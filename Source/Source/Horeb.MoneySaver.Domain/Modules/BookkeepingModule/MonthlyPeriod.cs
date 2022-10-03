using Horeb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Domain.Modules.BookkeepingModule
{
    public class MonthlyPeriod: BaseEntity
    {
        public DateTime StartDateUtc { get; set; }

        public DateTime EndDateUtc { get; set; }

        public bool IsDateWithin(DateTime date)
        {
            return date >= StartDateUtc && date.ToUniversalTime() <= EndDateUtc;
        }
    }
}
