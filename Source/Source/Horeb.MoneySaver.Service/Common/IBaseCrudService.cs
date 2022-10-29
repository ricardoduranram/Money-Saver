namespace Horeb.MoneySaver.Service;

public interface IBaseCrudService<TEntity>
{
    Task<TEntity> GetByIdAsync (int id);
    TEntity GetById (int id);
    IEnumerable<TEntity> GetAll (int pageNumer = 1, int pageSize = int.MaxValue);
    Task<TEntity> SoftDeleteAsync (int id);
    TEntity SoftDelete (int id);
    Task<TEntity> UpdateAsync (TEntity entity);
    TEntity Update (TEntity entity);
    void UpdateRange (IEnumerable<TEntity> entities);
    Task<TEntity> CreateAsync (TEntity entity);
    TEntity Create (TEntity entity);
}
