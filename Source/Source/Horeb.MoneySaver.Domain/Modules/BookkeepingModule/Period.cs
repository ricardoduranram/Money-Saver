using Horeb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Domain.Modules.BookkeepingModule
{
    public class Period: BaseEntity
    {
        public DateTime UtcStart { get; set; }

        public DateTime UtcEnd { get; set; }

        public bool IsDateWithin(DateTime date)
        {
            return date >= UtcStart && date.ToUniversalTime() <= UtcEnd;
        }
    }
}
