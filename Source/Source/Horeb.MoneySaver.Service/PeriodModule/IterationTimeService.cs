using Horeb.MoneySaver.Domain.Modules.PeriodModule;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;
using Horeb.MoneySaver.Service.Common;

namespace Horeb.MoneySaver.Service.PeriodModule;

public class IterationTimeService : BaseCrudService<IterationTime, IterationTimeModel>, IIterationTimeService
{
    readonly IRepository<IterationTime, IterationTimeModel> _repository;

    public IterationTimeService (IRepository<IterationTime, IterationTimeModel> repository)
        : base(repository) => this._repository = repository;

    public async Task<IterationTime?> GetByYearAndCycleNumber (int year, int cycleNumber) 
    {
        IEnumerable<IterationTime> iterationTime = await this._repository.GetByExpression(
            iteration => iteration.Year == year && iteration.CycleNumber == cycleNumber);

        return iterationTime.SingleOrDefault();
    }
}
