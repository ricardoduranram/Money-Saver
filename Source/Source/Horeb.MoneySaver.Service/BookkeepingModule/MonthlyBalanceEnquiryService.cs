using Horeb.Domain.FinanceModule;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;
using Horeb.MoneySaver.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Service.BookkeepingModule
{
    public class MonthlyBalanceEnquiryService 
        : BaseCrudService<MonthlyBalanceEnquiry, MonthlyBalanceEnquiryModel>, IMonthlyBalanceEnquiryService
    {
        private readonly IRepository<MonthlyBalanceEnquiry, MonthlyBalanceEnquiryModel> _repository;
        private readonly IMonthlyPeriodService _monthlyPeriodService;

        public MonthlyBalanceEnquiryService(
            IRepository<MonthlyBalanceEnquiry, MonthlyBalanceEnquiryModel> repository,
            IMonthlyPeriodService monthlyPeriodService): base(repository)
        {
            _repository = repository;
            _monthlyPeriodService = monthlyPeriodService;
        }

        public async Task<MonthlyBalanceEnquiry?> GetByMonthlyPeriodIdAndWalletId(
            int monthlyPeriodId,
            int walletId)
        {
            IEnumerable<MonthlyBalanceEnquiry> mbes = await _repository.GetByExpression((mbe) =>
                mbe.MonthlyPeriodId == monthlyPeriodId && mbe.WalletId == walletId);

            return mbes.SingleOrDefault();
        }

        public async Task<IEnumerable<MonthlyBalanceEnquiry>> GetByDateRangeForWalletId(
            (DateTime Start, DateTime End) dateRange,
            int walletId)
        {
            var monthlyPeriods = await this._monthlyPeriodService.GetByDateRange(dateRange);
            List<int> monthlyPeriodIds = monthlyPeriods.Select(period => period.Id).ToList();

            var mbes = await this._repository.GetByExpression((mbe) =>
                monthlyPeriodIds.Contains(mbe.MonthlyPeriodId)
                    && mbe.WalletId == walletId
            );

            return mbes;
        }

        public async Task AdjustBalancesFromDateRange(
            (DateTime Start, DateTime End) dateRange,
            int walletId,
            decimal adjustment)
        {
            
            IEnumerable<MonthlyBalanceEnquiry> balanceEnquiries = 
                await GetByDateRangeForWalletId(dateRange, walletId);
            var balancesToUpdate = balanceEnquiries.Select(balanceEnquiry => {
                balanceEnquiry.MonthlyEndingBalance += adjustment;
                return balanceEnquiry;
            });

            _repository.UpdateRange(balancesToUpdate);
        }
    }
}
