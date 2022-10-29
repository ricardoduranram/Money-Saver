using Horeb.Domain.TransactionModule;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;
using Horeb.MoneySaver.Service.Common;

namespace Horeb.MoneySaver.Service.BookkeepingModule;

public class TransactionCategoryService : BaseCrudService<TransactionCategory, TransactionCategoryModel>, ICategoryService
{
    private readonly IRepository<TransactionCategory, TransactionCategoryModel> _repository;

    public TransactionCategoryService (IRepository<TransactionCategory, TransactionCategoryModel> repository)
        : base(repository) => this._repository = repository;
}
