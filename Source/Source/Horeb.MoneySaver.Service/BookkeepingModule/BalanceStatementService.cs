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
    public class BalanceStatementService 
        : BaseCrudService<BalanceStatement, BalanceStatementModel>, IBalanceStatementService
    {
        private readonly IRepository<BalanceStatement, BalanceStatementModel> _repository;
        private readonly IPeriodService _periodService;

        public BalanceStatementService(
            IRepository<BalanceStatement, BalanceStatementModel> repository,
            IPeriodService periodService): base(repository)
        {
            _repository = repository;
            _periodService = periodService;
        }

        public async Task<BalanceStatement?> GetByMonthlyPeriodIdAndWalletId(
            int monthlyPeriodId,
            int walletId)
        {
            IEnumerable<BalanceStatement> mbes = await _repository.GetByExpression((mbe) =>
                mbe.PeriodId == monthlyPeriodId && mbe.WalletId == walletId);

            return mbes.SingleOrDefault();
        }

        public async Task<IEnumerable<BalanceStatement>> GetByDateRangeForWalletId(
            (DateTime Start, DateTime End) dateRange,
            int walletId)
        {

            var periods = await this._periodService.GetByDateRange(dateRange);
            List<int> periodIds = periods.Select(period => period.Id).ToList();

            var mbes = await this._repository.GetByExpression((mbe) =>
                periodIds.Contains(mbe.PeriodId)
                    && mbe.WalletId == walletId
            );

            return mbes;
        }

        public async Task AdjustBalancesFromDateRange(
            (DateTime Start, DateTime End) dateRange,
            int walletId,
            decimal adjustment)
        {
            
            IEnumerable<BalanceStatement> balanceEnquiries = 
                await GetByDateRangeForWalletId(dateRange, walletId);
            var balancesToUpdate = balanceEnquiries.Select(balanceStatement => {
                balanceStatement.Closing += adjustment;
                return balanceStatement;
            });

            _repository.UpdateRange(balancesToUpdate);
        }
    }
}
