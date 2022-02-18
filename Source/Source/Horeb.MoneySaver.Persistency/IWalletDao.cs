using Horeb.Domain.WalletModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.Pesistency
{
    public interface IWalletDao
    {
        Wallet Insert(string walletName);
        bool Update(Wallet wallet);
        bool Delete(string walletId);
        Wallet Find(string Id);
        List<Wallet> FindByName(string Name);
        List<Wallet> GetAll();        
    }
}
