using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horeb.MoneySaver.Persistency.EntityDataModels
{
    [Table(TableNames.BalanceStatement)]
    public class BalanceStatementModel: BaseEntity
    {
        public Decimal Opening { get; set; }
        public Decimal Closing { get; set; }

        [Required]
        [ForeignKey(TableNames.Wallet)]
        public int WalletId { get; set; }

        public virtual WalletModel? Wallet { get; set; }

        [Required]
        [ForeignKey(TableNames.Period)]
        public int PeriodId { get; set;}

        public virtual PeriodModel? Period { get; set; }
    }
}
