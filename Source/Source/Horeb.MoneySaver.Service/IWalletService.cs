using Horeb.Domain.WalletModule;
using Horeb.MoneySaver.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.Service
{
    public interface IWalletService: IBaseCrudService<Wallet>
    {
        Task<Wallet> CreateWithNameAsync(string walletName);
    }
}
