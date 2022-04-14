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
        Task<Wallet> FindByIdAsync(int id);        
        IEnumerable<Wallet> GetAll();
        Task<Wallet> CreateWithNameAsync(string walletName);
        Task<Wallet> SoftDeleteAsync(int id);
        Task<Wallet> SaveAsync(Wallet wallet);
    }
}
