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
    public class MonthlyPeriodService: BaseCrudService<MonthlyPeriod, MonthlyPeriodModel>, IMonthlyPeriodService
    {
        private IRepository<MonthlyPeriod, MonthlyPeriodModel> _repository;

        public MonthlyPeriodService(IRepository<MonthlyPeriod, MonthlyPeriodModel> repository)
            : base(repository)
        {
            _repository = repository;
        }

        //Return all MonthlyPeriods that are within the given date range (inclusive)
        public async Task<IEnumerable<MonthlyPeriod>> GetByDateRange((DateTime Start, DateTime End) dateRange)
        {
            var monthlyPeriods = await _repository.GetByExpression(period =>
                period.EndDateUtc >= dateRange.Start && period.EndDateUtc <= dateRange.End);
            return monthlyPeriods;
        }
    }
}
