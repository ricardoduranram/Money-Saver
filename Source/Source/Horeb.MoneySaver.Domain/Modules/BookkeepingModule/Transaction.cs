using Horeb.Infrastructure.Data;

namespace Horeb.Domain.TransactionModule;

public class Transaction : BaseEntity
{
    public Transaction () {
        this.Category = new TransactionCategory();
    }

    public decimal Amount { get; set; }

    public string Note { get; set; } = String.Empty;

    public int WalletId { get; set; }

    public TransactionCategory Category { get; set; }

    public DateTime UtcOccurredOn { get; set; }
}
