using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horeb.MoneySaver.Persistency.EntityDataModels
{
    [Table(TableNames.TrsansactionCategory)]
    public class TransactionCategoryModel: BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [ForeignKey(TableNames.Wallet)]
        public long WalletId { get; set; }

        public virtual WalletModel? Wallet { get; set; }

        public bool IsIncome { get; set; }
    }
}
