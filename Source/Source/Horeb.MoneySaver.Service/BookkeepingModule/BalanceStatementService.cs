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

        public async Task<BalanceStatement?> GetByIterationTimeIdAndWalletId(
            int iterationTimeId,
            int walletId)
        {
            IEnumerable<BalanceStatement> bes = await _repository.GetByExpression((be) =>
                be.IterationTimeId == iterationTimeId && be.WalletId == walletId);

            return bes.SingleOrDefault();
        }

        public async Task<IEnumerable<BalanceStatement>> GetByDateRangeForWalletId(
            (DateTime Start, DateTime End) dateRange,
            int walletId)
        {

            var periods = await this._periodService.GetByDateRangeAsync(dateRange);
            List<int> iterationTimeIds = periods
                .Where(period => period.HasIterationTime())
                .Select(period => period.IterationTimeId)
                .ToList();

            var balanceStatements = await this._repository.GetByExpression((mbe) =>
                iterationTimeIds.Contains(mbe.IterationTimeId)
                    && mbe.WalletId == walletId
            );

            return balanceStatements;
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
