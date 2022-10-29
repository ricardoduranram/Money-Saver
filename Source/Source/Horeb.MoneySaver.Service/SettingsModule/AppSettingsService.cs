using Horeb.MoneySaver.Domain.Modules.Settings;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;
using Horeb.MoneySaver.Service.Common;

namespace Horeb.MoneySaver.Service.SettingsModule;

public class AppSettingsService : BaseCrudService<AppSettings, AppSettingsModel>, IAppSettingsService
{
    private readonly IRepository<AppSettings, AppSettingsModel> _repository;

    public AppSettingsService (IRepository<AppSettings, AppSettingsModel> repository)
        : base(repository)
        => this._repository = repository;

    public AppSettings GetSingle () {
        var appSettings = GetAll(1, 1);

        return appSettings.Single();
    }

    public override Task<AppSettings> CreateAsync (AppSettings appSettings) {
        CheckCreatePrecondition(appSettings);

        return base.CreateAsync(appSettings);
    }

    public override AppSettings Create (AppSettings appSettings) {
        CheckCreatePrecondition(appSettings);

        return base.Create(appSettings);
    }

    private void CheckCreatePrecondition (AppSettings appSettings) {
        if (GetSingle() != null) {
            throw new InvalidOperationException($"{nameof(AppSettings)} Cannot be duplicated in Data Source");
        }
    }
}
