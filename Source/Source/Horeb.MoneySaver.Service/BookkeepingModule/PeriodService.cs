using Horeb.MoneySaver.Domain.Modules.BookkeepingModule;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;
using Horeb.MoneySaver.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Service.BookkeepingModule
{
    public class PeriodService: BaseCrudService<Period, PeriodModel>, IPeriodService
    {
        private IRepository<Period, PeriodModel> _repository;

        public PeriodService(IRepository<Period, PeriodModel> repository)
            : base(repository)
        {
            _repository = repository;
        }

        //Return all periods that are within the given date range (inclusive)
        public async Task<IEnumerable<Period>> GetByDateRange((DateTime Start, DateTime End) dateRange)
        {
            var periods = await _repository.GetByExpression(period =>
                period.UtcEnd >= dateRange.Start && period.UtcStart <= dateRange.End);
            return periods;
        }
    }
}
