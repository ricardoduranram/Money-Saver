using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horeb.MoneySaver.Persistency.EntityDataModels;

[Table(TableNames.BalanceStatement)]
public class BalanceStatementModel : Identity<int>
{
    [Required]
    public Decimal Opening { get; set; }

    [Required]
    public Decimal Closing { get; set; }

    [Required]
    [ForeignKey(TableNames.Wallet)]
    public int WalletId { get; set; }

    public virtual WalletModel? Wallet { get; set; }

    [Required]
    [ForeignKey(TableNames.IterationTime)]
    public int IterationTimeId { get; set; }

    public virtual IterationTimeModel? IterationTime { get; set; }
}
