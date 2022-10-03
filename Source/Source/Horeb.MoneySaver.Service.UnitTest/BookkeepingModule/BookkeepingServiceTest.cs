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
        private readonly Mock<IMonthlyBalanceEnquiryService> _balanceStatementServiceMock = new ();
        private readonly Mock<ITransactionCategoryService> _categoryServiceMock = new ();
        private readonly Mock<IMonthlyPeriodService> _periodServiceMock = new ();

        public BookkeepingServiceTest() {
            _walletServiceMock
                .Setup(walletS => walletS.Update(It.IsAny<Wallet>()))
                .Returns<Wallet>(result => result);

            MonthlyPeriod fakePeriod = GeneratePeriod();
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
                walletS => walletS.Update(It.Is<Wallet>(w => w.Amount == 700)));
        }

        [Fact(DisplayName = "Ability to debit Wallet Balance with recorded Transaction's amount")]
        [Trait("Category", "Unit")]
        public void ShouldDebitWalletBalanceWithRecordedTransactionAmount() {
            SetupDebitMockups();

            Transaction fakeTransaction = GenerateDebitTransaction();
            fakeTransaction.Amount = 500;

            _bookkeepingService.RecordTransactionAsync(fakeTransaction);
            _walletServiceMock.Verify(
                walletS => walletS.Update(It.Is<Wallet>(w => w.Amount == 1500)));
        }

        private void SetupCreditMockups()
        {
            TransactionCategory fakeCategory = GenerateCreditCategory();
            _categoryServiceMock
                .Setup(categoryS => categoryS.GetByIdAsync(fakeCategory.Id))
                .ReturnsAsync(fakeCategory);

            Wallet fakeWallet = GenerateWallet();
            fakeWallet.Amount = 1000;
            _walletServiceMock
                .Setup(walletS => walletS.GetByIdAsync(fakeWallet.Id))
                .ReturnsAsync(fakeWallet);
        }

        private void SetupDebitMockups()
        {
            TransactionCategory fakeCategory = GenerateDebitCategory();
            _categoryServiceMock
                .Setup(categoryS => categoryS.GetByIdAsync(fakeCategory.Id))
                .ReturnsAsync(fakeCategory);

            Wallet fakeWallet = GenerateWallet();
            fakeWallet.Amount = 1000;
            _walletServiceMock
                .Setup(walletS => walletS.GetByIdAsync(fakeWallet.Id))
                .ReturnsAsync(fakeWallet);
        }

        private Wallet GenerateWallet() {
            return new Wallet("Ricardo's Wallet") {
                Id = 1,
                Amount = 1000
            };
        }

        private MonthlyPeriod GeneratePeriod() {
            return new MonthlyPeriod()
            {
                Id = 10,
                StartDateUtc = new DateTime(2022, 9, 1),
                EndDateUtc = new DateTime(2022, 9, 30)
            };
        }

        private TransactionCategory GenerateCreditCategory() {
            return new TransactionCategory("Electronics") {
                Id = 20,
                WalletId = 1,
                IsIncome = false
            };

        }

        private TransactionCategory GenerateDebitCategory() {
            return new TransactionCategory("Salary")
            {
                Id = 21,
                WalletId = 1,
                IsIncome = true
            };
        }

        private Transaction GenerateCreditTransaction() {
            return new Transaction() {
                Id = 30,
                Amount = 300,
                Description = "Bought Monitor",
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
                Description = "Job Paycheck",
                CategoryId = 21,
                WalletId = 1,
                MonthlyPeriodId = 10,
                UtcOccurredOn = new DateTime(2022, 9, 15)
            };
        }

        private MonthlyBalanceEnquiry GenerateBalanceStatement() {
            return new MonthlyBalanceEnquiry
            {
                Id = 40,
                MonthlyEndingBalance = 200,
                MonthlyPeriodId = 10,
                WalletId = 1
            };
        }

        private List<MonthlyBalanceEnquiry> GenerateBalanceStatementList() {
            List<MonthlyBalanceEnquiry> balanceStatementList = new ();

            balanceStatementList.Add(GenerateBalanceStatement());
            balanceStatementList.Add(
                new MonthlyBalanceEnquiry { 
                    Id = 41,
                    MonthlyEndingBalance = 250,
                    MonthlyPeriodId = 10,
                    WalletId = 1,
            });

            return balanceStatementList;
        }
    }
}
