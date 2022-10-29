using Horeb.Infrastructure.Data;

namespace Horeb.Domain.WalletModule;

public class Wallet : BaseEntity
{
    public Wallet (string name) : base() {
        this.Name = name;
    }

    public string Name { get; set; }

    public Decimal Balance { get; set; }
}
