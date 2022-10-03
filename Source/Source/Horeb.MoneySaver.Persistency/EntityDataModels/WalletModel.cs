using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horeb.MoneySaver.Persistency.EntityDataModels
{
    [Table(TableNames.Wallet)]
    public class WalletModel: BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        public Decimal Amount { get; set; }

        public virtual List<TransactionCategoryModel> TransactionCategories { get; set; } 
            = new List<TransactionCategoryModel>();

        public virtual List<TransactionModel> Transactions { get; set; }
            = new List<TransactionModel>();

        public virtual List<MonthlyBalanceEnquiryModel> BalanceEnquiries { get; set; }
            = new List<MonthlyBalanceEnquiryModel>();
    }
}
