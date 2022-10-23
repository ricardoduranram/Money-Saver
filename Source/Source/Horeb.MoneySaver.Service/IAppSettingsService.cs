using Horeb.MoneySaver.Domain.Modules.Settings;
using Horeb.MoneySaver.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Service
{
    public interface IAppSettingsService : IBaseCrudService<AppSettings>
    {
        AppSettings GetSingle();
    }
}
