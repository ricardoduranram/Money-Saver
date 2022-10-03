using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horeb.MoneySaver.Persistency.EntityDataModels
{
    [Table(TableNames.BalanceEnquiry)]
    public class MonthlyBalanceEnquiryModel: BaseEntity
    {
        public Decimal MonthlyEndingBalance { get; set; }

        [Required]
        [ForeignKey(TableNames.Wallet)]
        public long WalletId { get; set; }

        public virtual WalletModel? Wallet { get; set; }

        [Required]
        [ForeignKey(TableNames.MonthlyPeriod)]
        public int MonthlyPeriodId { get; set;}

        public virtual MonthlyPeriodModel? MonthlyPeriod { get; set; }
    }
}
