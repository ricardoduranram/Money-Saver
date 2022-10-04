using AutoMapper;
using Horeb.Domain.FinanceModule;
using Horeb.Domain.TransactionModule;
using Horeb.Domain.WalletModule;
using Horeb.MoneySaver.Domain.Modules.BookkeepingModule;
using Horeb.MoneySaver.Persistency.EntityDataModels;

namespace Horeb.MoneySaver.API.Services.MappingServices
{
    public class RepositoryMappingProfile: Profile
    {
        public RepositoryMappingProfile()
        {
            CreateMap<Wallet, WalletModel>();
            CreateMap<WalletModel, Wallet>();
            CreateMap<Transaction, TransactionModel>();
            CreateMap<TransactionModel, Transaction>();
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();
            CreateMap<Period, PeriodModel>();
            CreateMap<PeriodModel, Period>();
            CreateMap<BalanceStatement, BalanceStatementModel>();
            CreateMap<BalanceStatementModel, BalanceStatement>();

        }
    }
}
