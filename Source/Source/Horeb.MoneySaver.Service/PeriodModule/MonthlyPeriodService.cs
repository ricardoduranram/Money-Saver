using Horeb.MoneySaver.Domain.Modules.BookkeepingModule;
using Horeb.MoneySaver.Domain.Modules.PeriodModule;
using Horeb.MoneySaver.Domain.Modules.Settings;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;
using Horeb.MoneySaver.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Service.PeriodModule
{
    public class MonthlyPeriodService : IPeriodService
    {        
        private readonly IIterationTimeService _iterationTimeService;
        private readonly IAppSettingsService _appSettingsService;

        public MonthlyPeriodService(
            IIterationTimeService iterationTimeService,
            IAppSettingsService appSettingsService)
        {
            _iterationTimeService = iterationTimeService;
            _appSettingsService = appSettingsService;
        }

        public async Task<MonthlyPeriod> GetByDateAsync(DateTime utcDate)
        {
            //For now we will assume the start day of the cycle is the first day of the month
            //this logic will change once configurations changes are supported.
            AppSettings appSettings = _appSettingsService.GetSingle();          
            Task<IterationTime?> getTterationTime =
                _iterationTimeService.GetByYearAndCycleNumber(utcDate.Year, utcDate.Month);            

            IterationTime? iterationTime = await getTterationTime;            
            
            DateTime periodStart = new (utcDate.Year, utcDate.Month, appSettings.CyclceStartDay);

            return new MonthlyPeriod(periodStart)
            {
                IterationTimeId = iterationTime?.Id ?? 0
            };           
        }

        public async Task<List<MonthlyPeriod>> GetByDateRangeAsync((DateTime UtcStart, DateTime UtcEnd) range)
        {
            //For now we will assume the start day of the cycle is the first day of the month
            //this logic will change once configurations changes are supported.
            List<MonthlyPeriod> montlyPeriods = new List<MonthlyPeriod>();

            MonthlyPeriod currentPeriod = await GetByDateAsync(range.UtcStart);
            montlyPeriods.Add(currentPeriod);
            do
            {
                IPeriodSpan nextPeriod = currentPeriod.Next();
                Task<IterationTime?> getTterationTime =
                _iterationTimeService.GetByYearAndCycleNumber(nextPeriod.UtcStart.Year, nextPeriod.UtcStart.Month);

                ((MonthlyPeriod)nextPeriod).IterationTimeId = getTterationTime?.Id ?? 0;
                montlyPeriods.Add((MonthlyPeriod)currentPeriod);
                currentPeriod = (MonthlyPeriod)nextPeriod;

            } while (currentPeriod.Includes(range.UtcEnd));

            return montlyPeriods;
        }
    }
}
