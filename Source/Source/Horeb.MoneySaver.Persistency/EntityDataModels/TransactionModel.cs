using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horeb.MoneySaver.Persistency.EntityDataModels;

[Table(TableNames.Transaction)]
[Index(nameof(UtcOccurredOn))]
public class TransactionModel : BaseEntity
{
    [Required]
    public decimal Amount { get; set; }

    public string Note { get; set; } = String.Empty;

    [Required]
    [ForeignKey(TableNames.Wallet)]
    public int WalletId { get; set; }

    public virtual WalletModel? Wallet { get; set; }

    [Required]
    [ForeignKey(TableNames.Category)]
    public int CategoryId { get; set; }

    public virtual TransactionCategoryModel? Category { get; set; }

    [Required]
    public DateTime UtcOccurredOn { get; set; }
}
