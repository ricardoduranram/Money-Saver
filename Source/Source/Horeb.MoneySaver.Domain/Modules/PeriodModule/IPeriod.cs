namespace Horeb.MoneySaver.Domain.Modules.PeriodModule;

public interface IPeriodSpan
{
    DateTime UtcStart { get; set; }

    DateTime UtcEnd { get; set; }

    bool Includes (DateTime utcDate);

    IPeriodSpan Next ();
}
