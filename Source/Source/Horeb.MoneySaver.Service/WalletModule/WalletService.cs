using Horeb.Domain.WalletModule;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;
using Horeb.MoneySaver.Service.Common;

namespace Horeb.Service.WalletModule;

public class WalletService : BaseCrudService<Wallet, WalletModel>, IWalletService
{
    private readonly IRepository<Wallet, WalletModel> _repository;

    public WalletService (IRepository<Wallet, WalletModel> repository) : base(repository)
        => this._repository = repository;

    /// <summary>Creates a wallet and returns a wallet from the Horeb data source.</summary>
    /// <param name="walletName">The user name (username) to create.</param>
    /// <returns> If successful return the newly created wallet. Returns an Emtpy wallet otherwise.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    public async Task<Wallet> CreateWithNameAsync (string walletName)
        => await this._repository.CreateAsync(new Wallet(walletName));
}
