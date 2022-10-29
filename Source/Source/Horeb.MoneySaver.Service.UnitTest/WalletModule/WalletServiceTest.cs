using FluentAssertions;
using Horeb.Domain.WalletModule;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;
using Horeb.Service.WalletModule;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Horeb.MoneySaver.Service.UnitTest;

public class WalletServiceTest
{
    private readonly WalletService _walletService;
    readonly Mock<IRepository<Wallet, WalletModel>> repositoryMoq = new();

    public WalletServiceTest () 
        => this._walletService = new WalletService(this.repositoryMoq.Object);

    public IEnumerable<Wallet> GenerateWalletsData () {
        List<Wallet> wallets = new();
        wallets.Add(
            new Wallet("Savings")
            {
                Balance = 500
            });
        wallets.Add(
            new Wallet("Cash")
            {
                Balance = 1000
            });

        return wallets;
    }

    [Fact(DisplayName = "Ability to return all wallet entities")]
    [Trait("Category", "Unit")]
    public void ShouldReturnAllWallets () {
        this.repositoryMoq.Setup(repository => repository.GetAll(1, int.MaxValue)).Returns(GenerateWalletsData());
        IEnumerable<Wallet> expectedWallets = this._walletService.GetAll();
        2.Should().Be(expectedWallets.Count());
    }
}
