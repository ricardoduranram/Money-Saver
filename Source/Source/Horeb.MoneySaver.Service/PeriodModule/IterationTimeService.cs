using Horeb.MoneySaver.Domain.Modules.PeriodModule;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;
using Horeb.MoneySaver.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Service.PeriodModule
{
    public class IterationTimeService : BaseCrudService<IterationTime, IterationTimeModel>, IIterationTimeService
    {
        IRepository<IterationTime, IterationTimeModel> _repository;

        public IterationTimeService(IRepository<IterationTime, IterationTimeModel> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<IterationTime?> GetByYearAndCycleNumber(int year, int cycleNumber)
        {
            IEnumerable<IterationTime> iterationTime = await _repository.GetByExpression(
                iteration => iteration.Year == year && iteration.CycleNumber == cycleNumber);

            return iterationTime.SingleOrDefault();
        }
    }
}
