using AutoMapper;
using Horeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Horeb.MoneySaver.Persistency;

public class Repository<TEntity, TModel> : IRepository<TEntity, TModel>
    where TEntity : class, IIdentity<int>
    where TModel : class, IIdentity<int>
{
    private readonly DapDbContext _dbContext;
    private readonly IMapper _mapper;

    public Repository (DapDbContext dapDbContext, IMapper mapper) {
        this._dbContext = dapDbContext;
        this._mapper = mapper;
    }

    public virtual bool Exists (int id) 
        => this._dbContext.Set<TModel>().Any(model => model.Id == id);

    public virtual IEnumerable<TEntity> GetAll (int pageNumber = 1, int pageSize = int.MaxValue) {
        var models = this._dbContext.Set<TModel>()
            .Skip(--pageNumber * pageSize)
            .Take(pageSize)
            .AsEnumerable();

        return this._mapper.Map<IEnumerable<TEntity>>(models);
    }

    public virtual async Task<TEntity> GetAsync (int id) {
        var model = await this._dbContext.Set<TModel>()
            .FindAsync(id);
        if (model == null) {
            return this._mapper.Map<TEntity>(model);
        }

        return this._mapper.Map<TEntity>(model);
    }

    public virtual TEntity Get (int id) {
        var model = this._dbContext.Set<TModel>()
            .Find(id);
        if (model == null) {
            return this._mapper.Map<TEntity>(model);
        }

        return this._mapper.Map<TEntity>(model);
    }

    public virtual async Task<TEntity> CreateAsync (TEntity entity) {
        var model = this._mapper.Map<TModel>(entity);

        await this._dbContext.AddAsync(model);
        await this._dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual TEntity Create (TEntity entity) {
        var model = this._mapper.Map<TModel>(entity);

        this._dbContext.Add(model);
        this._dbContext.SaveChanges();
        return entity;
    }

    public virtual async Task<TEntity> SoftDeleteAsync (int id) {
        var model = await this._dbContext.Set<TModel>()
            .FindAsync(id);

        //TODO Implement a true soft delete
        this._dbContext.Set<TModel>()
            .Remove(model);
        await this._dbContext.SaveChangesAsync();

        return this._mapper.Map<TEntity>(model);
    }

    public virtual TEntity SoftDelete (int id) {
        var model = this._dbContext.Set<TModel>()
            .Find(id);

        //TODO Implement a true soft delete
        this._dbContext.Set<TModel>()
            .Remove(model);
        this._dbContext.SaveChanges();

        return this._mapper.Map<TEntity>(model);
    }

    public virtual async Task<TEntity> UpdateAsync (TEntity entity) {
        var model = this._mapper.Map<TModel>(entity);

        this._dbContext.Update(model);
        await this._dbContext.SaveChangesAsync();

        return entity;
    }

    public virtual TEntity Update (TEntity entity) {
        var model = this._mapper.Map<TModel>(entity);

        this._dbContext.Update(model);
        this._dbContext.SaveChanges();

        return entity;
    }

    public virtual void UpdateRange (IEnumerable<TEntity> entities) {
        var models = this._mapper.Map<IEnumerable<TModel>>(entities);

        this._dbContext.UpdateRange(models);
    }

    public virtual async Task<IEnumerable<TEntity>> GetByExpression (
        Expression<Func<TModel, bool>> expression) {
        IEnumerable<TModel> entities =
            await Task.Run(() =>
                this._dbContext.Set<TModel>().Where(expression).OrderBy(entity => entity.Id).AsNoTracking());

        if (entities == null) {
            return Enumerable.Empty<TEntity>();
        }

        return this._mapper.Map<IEnumerable<TEntity>>(entities);
    }
}
