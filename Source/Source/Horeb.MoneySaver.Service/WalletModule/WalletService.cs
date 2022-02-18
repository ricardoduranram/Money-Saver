using Horeb.Domain.WalletModule;
using Horeb.Pesistency;

namespace Horeb.Service.WalletModule
{
    public class WalletService : IWalletService
    {
        private IWalletDao _walletDao;

        public WalletService(IWalletDao walletDao)
        {
            _walletDao = walletDao;
        }

        /// <summary>Creates a wallet and returns a wallet from the Horeb data source.</summary>
        /// <param name="walletName">The user name (username) to create.</param>
        /// <returns> If successful return the newly created wallet. Returns an Emtpy wallet otherwise.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        public Wallet CreateWithName(string walletName)
        {            
            return _walletDao.Insert(walletName);
        }

        /// <summary>Creates a wallet and returns a wallet from the Horeb data source.</summary>
        /// <param name="walletName">The user name (username) to create.</param>
        /// <returns> If successful return the newly created wallet. Returns an Emtpy wallet otherwise.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        public bool Delete(string id)
        {
            return _walletDao.Delete(id);
        }

        /// <summary>Gets a wallet where the id contains the specified id to match.</summary>
        /// <param name="id">The id to search for.</param>
        /// <returns>A <see cref="T:Horeb.Domain.WalletModule.Wallet" /> that contains the wallet to match the <paramref name="id" />
        public Wallet FindById(string id)
        {
            return _walletDao.Find(id);
        }

        /// <summary>Gets a wallet where the name contains the specified name to match.</summary>
        /// <param name="nameToMatch">The name to search for.</param>
        /// <returns>A List of <see cref="T:Horeb.Domain.WalletModule.Wallet" /> that contains the wallets to match the <paramref name="nameToMatch" />
        /// parameter.Leading and trailing spaces are trimmed from the <paramref name="nameToMatch" /> parameter value.</returns>
        public List<Wallet> FindByName(string nameToMatch)
        {            
            return _walletDao.FindByName(nameToMatch);
        }

        /// <summary>Gets a list of all the wallets in the data source.</summary>
        /// <returns>A List of <see cref="T:Horeb.Domain.WalletModule.Wallet" /> 
        /// of <see cref="T:Horeb.Domain.WalletModule.Wallet" /> objects representing all of the wallets in the data source.</returns>
        public List<Wallet> GetAll()
        {
            return _walletDao.GetAll();
        }

        /// <summary>Updates the wallet data to the data source.</summary>
        /// <returns> True if the user was saved successfully to the data source. False otherwise.</returns>         
        public bool Save(Wallet wallet)
        {
            return _walletDao.Update(wallet);
        }
    }
}
