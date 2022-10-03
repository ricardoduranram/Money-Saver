using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horeb.MoneySaver.Persistency.EntityDataModels
{
    [Table(TableNames.MonthlyPeriod)]
    public class MonthlyPeriodModel: BaseEntity
    {
        [Required]
        public DateTime StartDateUtc { get; set; }

        public DateTime EndDateUtc { get; set; }

        public virtual List<MonthlyBalanceEnquiryModel> BalanceEnquiries { get; set; }
            = new List<MonthlyBalanceEnquiryModel>();
    }
}
