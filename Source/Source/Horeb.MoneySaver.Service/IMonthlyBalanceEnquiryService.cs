using Horeb.Domain.FinanceModule;

namespace Horeb.MoneySaver.Service
{
    public interface IMonthlyBalanceEnquiryService: IBaseCrudService<MonthlyBalanceEnquiry>
    {
        Task<MonthlyBalanceEnquiry?> GetByMonthlyPeriodIdAndWalletId(
            int monthlyPeriodId,
            int walletId);

        //The Range is inclusive
        Task<IEnumerable<MonthlyBalanceEnquiry>> GetByDateRangeForWalletId(
            (DateTime Start, DateTime End) dateRange,
            int walletId);

        Task AdjustBalancesFromDateRange(
            (DateTime Start, DateTime End) dateRange,
            int walletId,
            decimal adjustment);
    }
}
