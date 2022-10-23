using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Domain.Common;

namespace Horeb.MoneySaver.Domain.Modules.Settings
{
    public class AppSettings : Identity<int>
    {
        public PeriodDurationType PeriodDuration { get; set; }

        public int CyclceStartDay { get; set; }
    }
}
