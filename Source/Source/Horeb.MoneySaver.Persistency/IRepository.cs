using System.Linq.Expressions;

namespace Horeb.MoneySaver.Persistency;

public interface IRepository<TEntity, TModel>
{
    IEnumerable<TEntity> GetAll (int pageIndex = 1, int pageSize = int.MaxValue);
    Task<TEntity> GetAsync (int id);
    TEntity Get (int id);
    bool Exists (int id);
    Task<TEntity> UpdateAsync (TEntity entity);
    TEntity Update (TEntity entity);
    void UpdateRange (IEnumerable<TEntity> entities);
    Task<TEntity> SoftDeleteAsync (int id);
    TEntity SoftDelete (int id);
    Task<TEntity> CreateAsync (TEntity entity);
    TEntity Create (TEntity entity);
    Task<IEnumerable<TEntity>> GetByExpression (
        Expression<Func<TModel, bool>> expression);
}
