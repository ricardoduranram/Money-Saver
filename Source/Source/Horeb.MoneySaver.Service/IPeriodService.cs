using Horeb.MoneySaver.Domain.Modules.BookkeepingModule;

namespace Horeb.MoneySaver.Service
{
    public interface IPeriodService
    {
        Task<MonthlyPeriod> GetByDateAsync(DateTime utcDate);

        Task<List<MonthlyPeriod>> GetByDateRangeAsync((DateTime UtcStart, DateTime UtcEnd) range);
    }
}
