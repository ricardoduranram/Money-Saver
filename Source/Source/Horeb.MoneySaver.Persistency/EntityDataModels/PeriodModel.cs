using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horeb.MoneySaver.Persistency.EntityDataModels
{
    [Table(TableNames.Period)]
    public class PeriodModel: BaseEntity
    {
        [Required]
        public DateTime UtcStart { get; set; }

        public DateTime UtcEnd { get; set; }

        public virtual List<BalanceStatementModel> BalanceStatements { get; set; }
            = new List<BalanceStatementModel>();
    }
}
