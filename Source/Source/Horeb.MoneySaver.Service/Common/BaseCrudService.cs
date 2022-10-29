using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency;

namespace Horeb.MoneySaver.Service.Common;

public abstract class BaseCrudService<TEntity, TModel> : IBaseCrudService<TEntity> where TEntity : IIdentity<int>
{
    private readonly IRepository<TEntity, TModel> _repository;

    public BaseCrudService (IRepository<TEntity, TModel> repository)
        => this._repository = repository;

    public virtual async Task<TEntity> SoftDeleteAsync (int id)
        => await this._repository.SoftDeleteAsync(id);

    public virtual TEntity SoftDelete (int id)
        => this._repository.SoftDelete(id);

    /// <summary>Gets an entity where the id contains the specified id to match.</summary>
    /// <param name="id">The id to search for.</param>
    /// <returns>A <see cref="T:Horeb.Infrastructure.Data.BaseEntity" /> that contains the entity to match the <paramref name="id" />
    public virtual async Task<TEntity> GetByIdAsync (int id)
        => await this._repository.GetAsync(id);

    public virtual TEntity GetById (int id)
        => this._repository.Get(id);

    /// <summary>Gets a list of all the entities in the data source.</summary>
    /// <returns>A List of <see cref="T:Horeb.Infrastructure.Data.BaseEntity" /> 
    /// of <see cref="T:Horeb.Infrastructure.Data.BaseEntity" /> objects representing all of the entities in the data source.</returns>
    public virtual IEnumerable<TEntity> GetAll (int pageNumer = 1, int pageSize = int.MaxValue)
        => this._repository.GetAll();

    /// <summary>Updates the entity data to the data source.</summary>
    /// <returns> True if the user was saved successfully to the data source. False otherwise.</returns>         
    public virtual async Task<TEntity> UpdateAsync (TEntity entity)
        => await this._repository.UpdateAsync(entity);

    public virtual TEntity Update (TEntity entity)
        => this._repository.Update(entity);

    public virtual void UpdateRange (IEnumerable<TEntity> entities)
        => this._repository.UpdateRange(entities);

    public virtual async Task<TEntity> CreateAsync (TEntity entity)
        => await this._repository.CreateAsync(entity);

    public virtual TEntity Create (TEntity entity)
        => this._repository.Create(entity);
}
