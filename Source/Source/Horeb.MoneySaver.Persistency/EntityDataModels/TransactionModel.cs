using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horeb.MoneySaver.Persistency.EntityDataModels
{
    [Table(TableNames.Transaction)]
    public class TransactionModel : BaseEntity
    {
        [Required]
        public decimal Amount { get; set; }

        public string Description { get; set; } = String.Empty;

        [Required]
        [ForeignKey(TableNames.Wallet)]
        public long WalletId { get; set; }

        public virtual WalletModel? Wallet { get; set; }

        [Required]
        [ForeignKey(TableNames.TrsansactionCategory)]
        public long TransactionCategoryId { get; set; }

        public virtual TransactionCategoryModel? TransactionCategory { get; set; }

        [Required]
        [ForeignKey(TableNames.MonthlyPeriod)]
        public long MonthlyPeriodId { get; set; }

        public virtual MonthlyPeriodModel?  MonthlyPeriod { get; set; }

        [Required]
        public DateTime UtcOccurredOn { get; set; }
    }
}
