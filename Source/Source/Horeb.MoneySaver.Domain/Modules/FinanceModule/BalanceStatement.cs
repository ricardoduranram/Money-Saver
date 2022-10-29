using Horeb.Infrastructure.Data;

namespace Horeb.Domain.FinanceModule;

public class BalanceStatement : Identity<int>
{
    public Decimal Opening { get; set; }

    public Decimal Closing { get; set; }

    public int IterationTimeId { get; set; }

    public int WalletId { get; set; }
}
