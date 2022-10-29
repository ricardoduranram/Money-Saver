using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horeb.MoneySaver.Persistency.EntityDataModels;

[Table(TableNames.IterationTime)]
public class IterationTimeModel : Identity<int>
{
    [Required]
    public int Year { get; set; }

    [Required]
    public int CycleNumber { get; set; }
}
