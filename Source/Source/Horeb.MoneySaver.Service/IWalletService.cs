using Horeb.Domain.WalletModule;
using Horeb.MoneySaver.Service;

namespace Horeb.Service;

public interface IWalletService : IBaseCrudService<Wallet>
{
    Task<Wallet> CreateWithNameAsync (string walletName);
}
