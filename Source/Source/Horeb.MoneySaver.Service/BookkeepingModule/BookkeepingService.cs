using Horeb.Domain.FinanceModule;
using Horeb.Domain.TransactionModule;
using Horeb.Domain.WalletModule;
using Horeb.MoneySaver.Domain.Modules.BookkeepingModule;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;
using Horeb.Service;

namespace Horeb.MoneySaver.Service.BookkeepingModule
{
    public class BookkeepingService : IBookkeepingService
    {
        private readonly IRepository<Transaction, TransactionModel> _repository;
        private readonly IWalletService _walletService;
        private readonly IBalanceStatementService _balanceStatementService;
        private readonly ICategoryService _categoryService;
        private readonly IPeriodService _periodService;


        public BookkeepingService(
            IRepository<Transaction, TransactionModel> repository,
            IWalletService walletService,
            IBalanceStatementService balanceStatementService,
            ICategoryService categoryService,
            IPeriodService periodService)
        {
            _repository = repository;
            _walletService = walletService;
            _balanceStatementService = balanceStatementService;
            _categoryService = categoryService;
            _periodService = periodService;
        }

        //Currently is not the booking service responsibility to create the balance statement or the period
        public async Task RecordTransactionAsync(Transaction transaction)
        {
            CheckTransactionIntegrity(transaction);

            Task<Wallet> getWalletTask = _walletService.GetByIdAsync(transaction.WalletId);
            Task<Category> getTransactionCategoryTask =
                _categoryService.GetByIdAsync(transaction.CategoryId);
            Task<Transaction> createTask = _repository.CreateAsync(transaction);
                        
            Category transactionCategory = await getTransactionCategoryTask;
            decimal adjustment = transactionCategory.ConvertAmmountToExpenseOrIncome(transaction.Amount);

            Task balancesAdjustmentTask =
                _balanceStatementService
                    .AdjustBalancesFromDateRange(
                    (transaction.UtcOccurredOn, DateTime.UtcNow),
                    transaction.WalletId,
                    adjustment);
            
            Wallet wallet = await getWalletTask;
            wallet.Balance += adjustment;
           
            _walletService.Update(wallet);
            await createTask;
            await balancesAdjustmentTask;
        }

        private void CheckTransactionIntegrity(Transaction transaction)
        {
            if (transaction == null) { 
                throw new ArgumentNullException(nameof(transaction), $"Is null or empty"); 
            }
            if (transaction.Amount < 0) { 
                throw new ArgumentOutOfRangeException($"Amount out of range {transaction.Amount}"); 
            }

            Period period =
                _periodService.GetById(transaction.MonthlyPeriodId);

            if (!period.IsDateWithin(transaction.UtcOccurredOn))
            {
                throw new ArgumentException(
                    $"{nameof(transaction)} date of occurrance does not match assigned MonthlyPeriodId");
            }
        }
    }
}
