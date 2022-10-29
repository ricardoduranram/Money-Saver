using Horeb.MoneySaver.Domain.Modules.Settings;

namespace Horeb.MoneySaver.Service;

public interface IAppSettingsService : IBaseCrudService<AppSettings>
{
    AppSettings GetSingle ();
}
