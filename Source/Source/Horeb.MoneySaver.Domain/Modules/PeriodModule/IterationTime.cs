using Horeb.Infrastructure.Data;

namespace Horeb.MoneySaver.Domain.Modules.PeriodModule;

public class IterationTime : Identity<int>
{
    public int Year { get; set; }

    public int CycleNumber { get; set; }
}
