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

namespace Horeb.MoneySaver.Service.UnitTest.BookkeepingModule
{
    public class BookkeepingServiceTest
    {
        private readonly IBookkeepingService _bookkeepingService;
        private readonly Mock<IRepository<Transaction, TransactionModel>> _repositoryMock = new ();
        private readonly Mock<IWalletService> _walletServiceMock = new ();
        private readonly Mock<IBalanceStatementService> _balanceStatementServiceMock = new ();
        private readonly Mock<ICategoryService> _categoryServiceMock = new ();
        private readonly Mock<IPeriodService> _periodServiceMock = new ();

        public BookkeepingServiceTest() {
            _walletServiceMock
                .Setup(walletS => walletS.Update(It.IsAny<Wallet>()))
                .Returns<Wallet>(result => result);

            Period fakePeriod = GeneratePeriod();
            _periodServiceMock
                .Setup(periodS => periodS.GetById(fakePeriod.Id))
                .Returns(fakePeriod);

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
        public void ShouldCreditWalletBalanceWithRecordedTransactionAmount() {
            SetupCreditMockups();

            Transaction fakeTransaction = GenerateCreditTransaction();
            fakeTransaction.Amount = 300;

            _bookkeepingService.RecordTransactionAsync(fakeTransaction);
            _walletServiceMock.Verify(
                walletS => walletS.Update(It.Is<Wallet>(w => w.Balance == 700)));
        }

        [Fact(DisplayName = "Ability to debit Wallet Balance with recorded Transaction's amount")]
        [Trait("Category", "Unit")]
        public void ShouldDebitWalletBalanceWithRecordedTransactionAmount() {
            SetupDebitMockups();

            Transaction fakeTransaction = GenerateDebitTransaction();
            fakeTransaction.Amount = 500;

            _bookkeepingService.RecordTransactionAsync(fakeTransaction);
            _walletServiceMock.Verify(
                walletS => walletS.Update(It.Is<Wallet>(w => w.Balance == 1500)));
        }

        private void SetupCreditMockups()
        {
            Category fakeCategory = GenerateCreditCategory();
            _categoryServiceMock
                .Setup(categoryS => categoryS.GetByIdAsync(fakeCategory.Id))
                .ReturnsAsync(fakeCategory);

            Wallet fakeWallet = GenerateWallet();
            fakeWallet.Balance = 1000;
            _walletServiceMock
                .Setup(walletS => walletS.GetByIdAsync(fakeWallet.Id))
                .ReturnsAsync(fakeWallet);
        }

        private void SetupDebitMockups()
        {
            Category fakeCategory = GenerateDebitCategory();
            _categoryServiceMock
                .Setup(categoryS => categoryS.GetByIdAsync(fakeCategory.Id))
                .ReturnsAsync(fakeCategory);

            Wallet fakeWallet = GenerateWallet();
            fakeWallet.Balance = 1000;
            _walletServiceMock
                .Setup(walletS => walletS.GetByIdAsync(fakeWallet.Id))
                .ReturnsAsync(fakeWallet);
        }

        private Wallet GenerateWallet() {
            return new Wallet("Ricardo's Wallet") {
                Id = 1,
                Balance = 1000
            };
        }

        private Period GeneratePeriod() {
            return new Period()
            {
                Id = 10,
                UtcStart = new DateTime(2022, 9, 1),
                UtcEnd = new DateTime(2022, 9, 30)
            };
        }

        private Category GenerateCreditCategory() {
            return new Category("Electronics") {
                Id = 20,
                WalletId = 1,
                Type = CategoryType.Expense
            };

        }

        private Category GenerateDebitCategory() {
            return new Category("Salary")
            {
                Id = 21,
                WalletId = 1,
                Type = CategoryType.Income
            };
        }

        private Transaction GenerateCreditTransaction() {
            return new Transaction() {
                Id = 30,
                Amount = 300,
                Note = "Bought Monitor",
                CategoryId = 20,
                WalletId = 1,
                MonthlyPeriodId = 10,
                UtcOccurredOn = new DateTime(2022, 9, 15)
            };
        }

        private Transaction GenerateDebitTransaction()
        {
            return new Transaction()
            {
                Id = 31,
                Amount = 500,
                Note = "Job Paycheck",
                CategoryId = 21,
                WalletId = 1,
                MonthlyPeriodId = 10,
                UtcOccurredOn = new DateTime(2022, 9, 15)
            };
        }

        private BalanceStatement GenerateBalanceStatement() {
            return new BalanceStatement
            {
                Id = 40,
                Closing = 200,
                PeriodId = 10,
                WalletId = 1
            };
        }

        private List<BalanceStatement> GenerateBalanceStatementList() {
            List<BalanceStatement> balanceStatementList = new ();

            balanceStatementList.Add(GenerateBalanceStatement());
            balanceStatementList.Add(
                new BalanceStatement { 
                    Id = 41,
                    Closing = 250,
                    PeriodId = 10,
                    WalletId = 1,
            });

            return balanceStatementList;
        }
    }
}
