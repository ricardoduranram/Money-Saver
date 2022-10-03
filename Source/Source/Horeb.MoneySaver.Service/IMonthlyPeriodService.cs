using Horeb.MoneySaver.Domain.Modules.BookkeepingModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Service
{
    public interface IMonthlyPeriodService: IBaseCrudService<MonthlyPeriod>
    {
        //Range is inclusive
        Task<IEnumerable<MonthlyPeriod>> GetByDateRange((DateTime Start, DateTime End) dateRange);
    }
}
