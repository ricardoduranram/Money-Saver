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
        private readonly IMonthlyBalanceEnquiryService _balanceEnquiryService;
        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly IMonthlyPeriodService _monthlyPeriodService;


        public BookkeepingService(
            IRepository<Transaction, TransactionModel> repository,
            IWalletService walletService,
            IMonthlyBalanceEnquiryService balanceEnquiryService,
            ITransactionCategoryService transactionCategoryService,
            IMonthlyPeriodService monthlyPeriodService)
        {
            _repository = repository;
            _walletService = walletService;
            _balanceEnquiryService = balanceEnquiryService;
            _transactionCategoryService = transactionCategoryService;
            _monthlyPeriodService = monthlyPeriodService;
        }

        //Currently is not the booking service responsibility to create the balance statement or the period
        public async Task RecordTransactionAsync(Transaction transaction)
        {
            CheckTransactionIntegrity(transaction);

            Task<Wallet> getWalletTask = _walletService.GetByIdAsync(transaction.WalletId);
            Task<TransactionCategory> getTransactionCategoryTask =
                _transactionCategoryService.GetByIdAsync(transaction.CategoryId);
            Task<Transaction> createTask = _repository.CreateAsync(transaction);
                        
            TransactionCategory transactionCategory = await getTransactionCategoryTask;
            decimal adjustment = transactionCategory.ConvertAmmountToExpenseOrIncome(transaction.Amount);

            Task balancesAdjustmentTask =
                _balanceEnquiryService
                    .AdjustBalancesFromDateRange(
                    (transaction.UtcOccurredOn, DateTime.UtcNow),
                    transaction.WalletId,
                    adjustment);
            
            Wallet wallet = await getWalletTask;
            wallet.Amount += adjustment;
           
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

            MonthlyPeriod period =
                _monthlyPeriodService.GetById(transaction.MonthlyPeriodId);

            if (!period.IsDateWithin(transaction.UtcOccurredOn))
            {
                throw new ArgumentException(
                    $"{nameof(transaction)} date of occurrance does not match assigned MonthlyPeriodId");
            }
        }
    }
}
