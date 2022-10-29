using Horeb.Domain.FinanceModule;
using Horeb.Domain.TransactionModule;
using Horeb.Domain.WalletModule;
using Horeb.MoneySaver.Domain.Modules.BookkeepingModule;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;
using Horeb.MoneySaver.Service.BookkeepingModule;
using Horeb.Service;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Horeb.MoneySaver.Service.UnitTest.BookkeepingModule;

public class BookkeepingServiceTest
{
    private readonly IBookkeepingService _bookkeepingService;
    private readonly Mock<IRepository<Transaction, TransactionModel>> _repositoryMock = new();
    private readonly Mock<IWalletService> _walletServiceMock = new();
    private readonly Mock<IBalanceStatementService> _balanceStatementServiceMock = new();
    private readonly Mock<ICategoryService> _categoryServiceMock = new();
    private readonly Mock<IPeriodService> _periodServiceMock = new();

    public BookkeepingServiceTest () {
        _walletServiceMock
            .Setup(walletS => walletS.Update(It.IsAny<Wallet>()))
            .Returns<Wallet>(result => result);

        MonthlyPeriod fakePeriod = GeneratePeriod();
        _periodServiceMock
            .Setup(periodS => periodS.GetByDateAsync(It.IsAny<DateTime>()))
            .ReturnsAsync(fakePeriod);

        _bookkeepingService = new BookkeepingService(
        _repositoryMock.Object,
        _walletServiceMock.Object,
        _balanceStatementServiceMock.Object,
        _categoryServiceMock.Object,
        _periodServiceMock.Object
        );
    }

    [Fact(DisplayName = "Ability to credit Wallet Balance with recorded Transaction's amount")]
    [Trait("Category", "Unit")]
    public void ShouldCreditWalletBalanceWithRecordedTransactionAmount () {
        SetupCreditMockups();

        Transaction fakeTransaction = GenerateCreditTransaction();
        fakeTransaction.Amount = 300;

        _bookkeepingService.RecordTransactionAsync(fakeTransaction);
        _walletServiceMock.Verify(
            walletS => walletS.Update(It.Is<Wallet>(w => w.Balance == 700)));
    }

    [Fact(DisplayName = "Ability to debit Wallet Balance with recorded Transaction's amount")]
    [Trait("Category", "Unit")]
    public void ShouldDebitWalletBalanceWithRecordedTransactionAmount () {
        SetupDebitMockups();

        Transaction fakeTransaction = GenerateDebitTransaction();
        fakeTransaction.Amount = 500;

        _bookkeepingService.RecordTransactionAsync(fakeTransaction);
        _walletServiceMock.Verify(
            walletS => walletS.Update(It.Is<Wallet>(w => w.Balance == 1500)));
    }

    private void SetupCreditMockups () {
        TransactionCategory fakeCategory = GenerateCreditCategory();
        _categoryServiceMock
            .Setup(categoryS => categoryS.GetByIdAsync(fakeCategory.Id))
            .ReturnsAsync(fakeCategory);

        Wallet fakeWallet = GenerateWallet();
        fakeWallet.Balance = 1000;
        _walletServiceMock
            .Setup(walletS => walletS.GetByIdAsync(fakeWallet.Id))
            .ReturnsAsync(fakeWallet);
    }

    private void SetupDebitMockups () {
        TransactionCategory fakeCategory = GenerateDebitCategory();
        _categoryServiceMock
            .Setup(categoryS => categoryS.GetByIdAsync(fakeCategory.Id))
            .ReturnsAsync(fakeCategory);

        Wallet fakeWallet = GenerateWallet();
        fakeWallet.Balance = 1000;
        _walletServiceMock
            .Setup(walletS => walletS.GetByIdAsync(fakeWallet.Id))
            .ReturnsAsync(fakeWallet);
    }

    private Wallet GenerateWallet () {
        return new Wallet("Ricardo's Wallet")
        {
            Id = 1,
            Balance = 1000
        };
    }

    private MonthlyPeriod GeneratePeriod () {
        return new MonthlyPeriod(new DateTime(2022, 9, 1, 0, 0, 0, DateTimeKind.Utc))
        {
            UtcStart = new DateTime(2022, 9, 1),
            IterationTimeId = 10
        };
    }

    private TransactionCategory GenerateCreditCategory () {
        return new TransactionCategory("Electronics")
        {
            Id = 20,
            WalletId = 1,
            Type = CategoryType.Expense
        };

    }

    private TransactionCategory GenerateDebitCategory () {
        return new TransactionCategory("Salary")
        {
            Id = 21,
            WalletId = 1,
            Type = CategoryType.Income
        };
    }

    private Transaction GenerateCreditTransaction () {
        var transaction = new Transaction()
        {
            Id = 30,
            Amount = 300,
            Note = "Bought Monitor",
            WalletId = 1,
            UtcOccurredOn = new DateTime(2022, 9, 15)
        };
        transaction.Category.Id = 20;

        return transaction;
    }

    private Transaction GenerateDebitTransaction () {
        return new Transaction()
        {
            Id = 31,
            Amount = 500,
            Note = "Job Paycheck",
            Category = new TransactionCategory("Salary")
            {
                Id = 21,
                WalletId = 1,
                Type = CategoryType.Income
            },
            WalletId = 1,
            UtcOccurredOn = new DateTime(2022, 9, 15)
        };
    }

    private BalanceStatement GenerateBalanceStatement () {
        return new BalanceStatement
        {
            Id = 40,
            Closing = 200,
            IterationTimeId = 10,
            WalletId = 1
        };
    }

    private List<BalanceStatement> GenerateBalanceStatementList () {
        List<BalanceStatement> balanceStatementList = new();

        balanceStatementList.Add(GenerateBalanceStatement());
        balanceStatementList.Add(
            new BalanceStatement
            {
                Id = 41,
                Closing = 250,
                IterationTimeId = 11,
                WalletId = 1,
            });

        return balanceStatementList;
    }
}
