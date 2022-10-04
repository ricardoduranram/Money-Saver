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
    public class CategoryService: BaseCrudService<Category, CategoryModel>, ICategoryService
    {
        private readonly IRepository<Category, CategoryModel> _repository;

        public CategoryService(IRepository<Category, CategoryModel> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
