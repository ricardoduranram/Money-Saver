using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Persistency
{
    public interface IRepository<TEntity, TModel>
    {
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> SoftDeleteAsync(int id);
        Task<TEntity> GetAsync(int id);
        Task<TEntity> CreateAsync(TEntity entity);
        IEnumerable<TEntity> GetAll(int pageIndex = 1, int pageSize = int.MaxValue);
    }
}
