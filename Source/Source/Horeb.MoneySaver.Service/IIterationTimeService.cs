using Horeb.MoneySaver.Domain.Modules.PeriodModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Service
{
    public interface IIterationTimeService : IBaseCrudService<IterationTime>
    {
        Task<IterationTime?> GetByYearAndCycleNumber(int year, int cycleNumber);
    }
}
