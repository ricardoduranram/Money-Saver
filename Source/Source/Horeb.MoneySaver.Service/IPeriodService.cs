using Horeb.MoneySaver.Domain.Modules.BookkeepingModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Service
{
    public interface IPeriodService: IBaseCrudService<Period>
    {
        //Range is inclusive
        Task<IEnumerable<Period>> GetByDateRange((DateTime Start, DateTime End) dateRange);
    }
}
