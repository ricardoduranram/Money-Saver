using Horeb.MoneySaver.Domain.Modules.PeriodModule;

namespace Horeb.MoneySaver.Service;

public interface IIterationTimeService : IBaseCrudService<IterationTime>
{
    Task<IterationTime?> GetByYearAndCycleNumber (int year, int cycleNumber);
}
