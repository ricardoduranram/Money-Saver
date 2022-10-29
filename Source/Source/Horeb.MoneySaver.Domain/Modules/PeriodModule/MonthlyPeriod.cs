using Horeb.MoneySaver.Domain.Modules.PeriodModule;

namespace Horeb.MoneySaver.Domain.Modules.BookkeepingModule;

public class MonthlyPeriod : IPeriodSpan
{
    //This function will normalize time to 0
    public MonthlyPeriod (DateTime utcStart) {
        this.UtcStart = utcStart.Date;
        this.UtcEnd = utcStart.Date.AddMonths(1).AddDays(-1);
    }

    public DateTime UtcStart { get; set; }

    public DateTime UtcEnd { get; set; }

    public int IterationTimeId { get; set; }

    public bool HasIterationTime () => this.IterationTimeId > 0;

    public bool Includes (DateTime utcDate) {
        utcDate = utcDate.Date;
        return utcDate >= this.UtcStart && utcDate.ToUniversalTime() <= this.UtcEnd;
    }

    public IPeriodSpan Next () 
        => new MonthlyPeriod(this.UtcEnd.AddDays(1));
}
