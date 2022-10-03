using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Service.Common
{
    public abstract class BaseCrudService<TEntity, TModel> : IBaseCrudService<TEntity> where TEntity : BaseEntity
    {
        private readonly IRepository<TEntity, TModel> _repository;

        public BaseCrudService(IRepository<TEntity, TModel> repository)
        {
            _repository = repository;
        }

        public virtual async Task<TEntity> SoftDeleteAsync(int id)
        {
            return await _repository.SoftDeleteAsync(id);
        }

        /// <summary>Gets an entity where the id contains the specified id to match.</summary>
        /// <param name="id">The id to search for.</param>
        /// <returns>A <see cref="T:Horeb.Infrastructure.Data.BaseEntity" /> that contains the entity to match the <paramref name="id" />
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public virtual TEntity GetById(int id) {
            return _repository.Get(id);
        }

        /// <summary>Gets a list of all the entities in the data source.</summary>
        /// <returns>A List of <see cref="T:Horeb.Infrastructure.Data.BaseEntity" /> 
        /// of <see cref="T:Horeb.Infrastructure.Data.BaseEntity" /> objects representing all of the entities in the data source.</returns>
        public virtual IEnumerable<TEntity> GetAll(int pageNumer = 1, int pageSize = int.MaxValue)
        {
            return _repository.GetAll();
        }

        /// <summary>Updates the entity data to the data source.</summary>
        /// <returns> True if the user was saved successfully to the data source. False otherwise.</returns>         
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            return _repository.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _repository.UpdateRange(entities);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            return await _repository.CreateAsync(entity);
        }
    }
}
