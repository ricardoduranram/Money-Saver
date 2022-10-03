using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Service
{
    public interface IBaseCrudService<TEntity>
    {
        Task<TEntity> GetByIdAsync(int id);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll(int pageNumer = 1, int pageSize = int.MaxValue);
        Task<TEntity> SoftDeleteAsync(int id);
        Task<TEntity> UpdateAsync(TEntity entity);
        TEntity Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        Task<TEntity> CreateAsync(TEntity entity);
    }
}
