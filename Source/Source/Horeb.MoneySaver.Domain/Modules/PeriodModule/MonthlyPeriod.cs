using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Domain.Modules.PeriodModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Domain.Modules.BookkeepingModule
{
    public class MonthlyPeriod : IPeriodSpan
    {        
        //This function will normalize time to 0
        public MonthlyPeriod(DateTime utcStart)
        {
            UtcStart = utcStart.Date;
            UtcEnd = utcStart.Date.AddMonths(1).AddDays(-1);
        }

        public DateTime UtcStart { get; set; }

        public DateTime UtcEnd { get; set; }

        public int IterationTimeId { get; set; }

        public bool HasIterationTime()
        {
            return IterationTimeId > 0;
        }

        public bool Includes(DateTime utcDate)
        {
            utcDate = utcDate.Date;
            return utcDate >= UtcStart && utcDate.ToUniversalTime() <= UtcEnd;
        }

        public IPeriodSpan Next()
        {
            return new MonthlyPeriod(UtcEnd.AddDays(1));
        }       
    }
}
