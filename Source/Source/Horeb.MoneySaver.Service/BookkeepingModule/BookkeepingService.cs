﻿using Horeb.Domain.FinanceModule;
using Horeb.Domain.TransactionModule;
using Horeb.Domain.WalletModule;
using Horeb.MoneySaver.Domain.Modules.BookkeepingModule;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;
using Horeb.Service;

namespace Horeb.MoneySaver.Service.BookkeepingModule;

public class BookkeepingService : IBookkeepingService
{
    private readonly IRepository<Transaction, TransactionModel> _repository;
    private readonly IWalletService _walletService;
    private readonly IBalanceStatementService _balanceStatementService;
    private readonly ICategoryService _categoryService;
    private readonly IPeriodService _periodService;

    public BookkeepingService (
        IRepository<Transaction, TransactionModel> repository,
        IWalletService walletService,
        IBalanceStatementService balanceStatementService,
        ICategoryService categoryService,
        IPeriodService periodService) {
        this._repository = repository;
        this._walletService = walletService;
        this._balanceStatementService = balanceStatementService;
        this._categoryService = categoryService;
        this._periodService = periodService;
    }

    //Currently is not the booking service responsibility to create the balance statement or the period
    public async Task RecordTransactionAsync (Transaction transaction) {
        await CheckTransactionIntegrity(transaction);

        Task<Wallet> getWalletTask = this._walletService.GetByIdAsync(transaction.WalletId);
        Task<TransactionCategory> getTransactionCategoryTask =
            this._categoryService.GetByIdAsync(transaction.Category.Id);
        Task<Transaction> createTask = this._repository.CreateAsync(transaction);

        TransactionCategory transactionCategory = await getTransactionCategoryTask;
        decimal adjustment = transactionCategory.ConvertAmmountToExpenseOrIncome(transaction.Amount);

        Task balancesAdjustmentTask =
            this._balanceStatementService
                .AdjustBalancesFromDateRange(
                (transaction.UtcOccurredOn, DateTime.UtcNow),
                transaction.WalletId,
                adjustment);

        Wallet wallet = await getWalletTask;
        wallet.Balance += adjustment;

        this._walletService.Update(wallet);
        await createTask;
        await balancesAdjustmentTask;
    }

    private async Task CheckTransactionIntegrity (Transaction transaction) {
        if (transaction == null) {
            throw new ArgumentNullException(nameof(transaction), $"Is null or empty");
        }

        if (transaction.Amount < 0) {
            throw new ArgumentOutOfRangeException($"Amount out of range {transaction.Amount}");
        }

        MonthlyPeriod period =
            await this._periodService.GetByDateAsync(transaction.UtcOccurredOn);

        if (period == null) {
            throw new InvalidOperationException($"{nameof(Transaction)} There must be a period in which Transaction resides.");
        }
    }
}
