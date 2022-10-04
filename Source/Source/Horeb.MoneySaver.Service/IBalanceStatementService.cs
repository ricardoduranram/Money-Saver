using Horeb.Domain.FinanceModule;

namespace Horeb.MoneySaver.Service
{
    public interface IBalanceStatementService: IBaseCrudService<BalanceStatement>
    {
        Task<BalanceStatement?> GetByMonthlyPeriodIdAndWalletId(
            int monthlyPeriodId,
            int walletId);

        //The Range is inclusive
        Task<IEnumerable<BalanceStatement>> GetByDateRangeForWalletId(
            (DateTime Start, DateTime End) dateRange,
            int walletId);

        Task AdjustBalancesFromDateRange(
            (DateTime Start, DateTime End) dateRange,
            int walletId,
            decimal adjustment);
    }
}
