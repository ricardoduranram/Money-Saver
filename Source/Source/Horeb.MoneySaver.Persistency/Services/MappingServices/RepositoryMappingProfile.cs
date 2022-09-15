using AutoMapper;
using Horeb.Domain.TransactionModule;
using Horeb.Domain.WalletModule;
using Horeb.MoneySaver.Persistency.EntityDataModels;

namespace Horeb.MoneySaver.API.Services.MappingServices
{
    public class RepositoryMappingProfile: Profile
    {
        public RepositoryMappingProfile()
        {
            CreateMap<Wallet, WalletModel>();
            CreateMap<Transaction, TransactionModel>();
            CreateMap<TransactionCategory, TransactionCategoryModel>();
        }
    }
}
