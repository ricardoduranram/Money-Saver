using AutoMapper;
using Horeb.Domain.WalletModule;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;

namespace Horeb.Service.WalletModule
{
    public class WalletService : IWalletService
    {
        private readonly IRepository<Wallet, WalletModel> _repository;

        public WalletService(IRepository<Wallet, WalletModel> repository)
        {
            _repository = repository;
        }

        /// <summary>Creates a wallet and returns a wallet from the Horeb data source.</summary>
        /// <param name="walletName">The user name (username) to create.</param>
        /// <returns> If successful return the newly created wallet. Returns an Emtpy wallet otherwise.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        public async Task<Wallet> CreateWithNameAsync(string walletName)
        {            
            return await _repository.CreateAsync(new Wallet(walletName));
        }

        /// <summary>Creates a wallet and returns a wallet from the Horeb data source.</summary>
        /// <param name="walletName">The user name (username) to create.</param>
        /// <returns> If successful return the newly created wallet. Returns an Emtpy wallet otherwise.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        public async Task<Wallet> SoftDeleteAsync(int id)
        {
            return await _repository.SoftDeleteAsync(id);
        }

        /// <summary>Gets a wallet where the id contains the specified id to match.</summary>
        /// <param name="id">The id to search for.</param>
        /// <returns>A <see cref="T:Horeb.Domain.WalletModule.Wallet" /> that contains the wallet to match the <paramref name="id" />
        public async Task<Wallet> FindByIdAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        /// <summary>Gets a list of all the wallets in the data source.</summary>
        /// <returns>A List of <see cref="T:Horeb.Domain.WalletModule.Wallet" /> 
        /// of <see cref="T:Horeb.Domain.WalletModule.Wallet" /> objects representing all of the wallets in the data source.</returns>
        public IEnumerable<Wallet> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>Updates the wallet data to the data source.</summary>
        /// <returns> True if the user was saved successfully to the data source. False otherwise.</returns>         
        public async Task<Wallet> SaveAsync(Wallet wallet)
        {
            return await _repository.UpdateAsync(wallet);
        }
    }
}
