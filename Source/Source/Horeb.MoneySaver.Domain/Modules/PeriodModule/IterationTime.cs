using Horeb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Domain.Modules.PeriodModule
{
    public class IterationTime : Identity<int>
    {
        public int Year { get; set; }

        public int CycleNumber { get; set; }
    }
}
