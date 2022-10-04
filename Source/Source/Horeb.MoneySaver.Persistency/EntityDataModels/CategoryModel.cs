using Horeb.Domain.TransactionModule;
using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horeb.MoneySaver.Persistency.EntityDataModels
{
    [Table(TableNames.Category)]
    public class CategoryModel: BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [ForeignKey(TableNames.Wallet)]
        public int WalletId { get; set; }

        public virtual WalletModel? Wallet { get; set; }

        public CategoryType Type { get; set; }
    }
}
