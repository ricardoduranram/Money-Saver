using Horeb.Domain.TransactionModule;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Persistency.EntityDataModels;
using Horeb.MoneySaver.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Service.BookkeepingModule
{
    public class TransactionCategoryService: BaseCrudService<TransactionCategory, TransactionCategoryModel>, ITransactionCategoryService
    {
        private readonly IRepository<TransactionCategory, TransactionCategoryModel> _repository;

        public TransactionCategoryService(IRepository<TransactionCategory, TransactionCategoryModel> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
