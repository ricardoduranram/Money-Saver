using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Persistency
{
    public interface IRepository<TEntity, TModel>
    {
        IEnumerable<TEntity> GetAll(int pageIndex = 1, int pageSize = int.MaxValue);
        Task<TEntity> GetAsync(int id);
        TEntity Get(int id);
        bool Exists(int id);
        Task<TEntity> UpdateAsync(TEntity entity);
        TEntity Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        Task<TEntity> SoftDeleteAsync(int id);        
        Task<TEntity> CreateAsync(TEntity entity);        
        Task<IEnumerable<TEntity>> GetByExpression(
            Expression<Func<TModel, bool>> expression);
    }
}
