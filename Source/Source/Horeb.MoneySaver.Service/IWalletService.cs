using Horeb.Domain.WalletModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.Service
{
    public interface IWalletService
    {
        Wallet FindById(string id);
        List<Wallet> FindByName(string nameToMatch);
        List<Wallet> GetAll();
        Wallet CreateWithName(string walletName);
        bool Delete(string id);
        bool Save(Wallet wallet);
    }
}
