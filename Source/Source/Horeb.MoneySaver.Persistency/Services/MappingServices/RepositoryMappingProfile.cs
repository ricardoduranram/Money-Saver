using AutoMapper;
using Horeb.Domain.FinanceModule;
using Horeb.Domain.TransactionModule;
using Horeb.Domain.WalletModule;
using Horeb.MoneySaver.Domain.Modules.BookkeepingModule;
using Horeb.MoneySaver.Domain.Modules.PeriodModule;
using Horeb.MoneySaver.Domain.Modules.Settings;
using Horeb.MoneySaver.Persistency.EntityDataModels;

namespace Horeb.MoneySaver.API.Services.MappingServices
{
    public class RepositoryMappingProfile: Profile
    {
        public RepositoryMappingProfile()
        {
            CreateMap<Wallet, WalletModel>();
            CreateMap<WalletModel, Wallet>();

            CreateMap<Transaction, TransactionModel>()
                .ForMember(dest => dest.Id, (opt => opt.MapFrom(src => src.Category.Id)));
            CreateMap<TransactionModel, Transaction>();

            CreateMap<TransactionCategory, TransactionCategoryModel>();
            CreateMap<TransactionCategoryModel, TransactionCategory>();   
            
            CreateMap<BalanceStatement, BalanceStatementModel>();
            CreateMap<BalanceStatementModel, BalanceStatement>();

            CreateMap<IterationTimeModel, IterationTime>();
            CreateMap<IterationTime, IterationTimeModel>();

            CreateMap<AppSettingsModel, AppSettings>();
            CreateMap<AppSettings, AppSettingsModel>();

        }
    }
}
