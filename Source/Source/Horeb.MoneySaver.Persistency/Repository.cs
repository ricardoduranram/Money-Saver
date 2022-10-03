using AutoMapper;
using Horeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Horeb.MoneySaver.Persistency
{
    public class Repository<TEntity, TModel> : IRepository<TEntity, TModel> 
        where TEntity : class, IValue<int>
        where TModel : class, IValue<int>
    {
        private readonly DapDbContext _dbContext;
        private readonly IMapper _mapper;

        public Repository(DapDbContext dapDbContext, IMapper mapper)
        {
            _dbContext = dapDbContext;
            _mapper = mapper;
        }

        public bool Exists(int id)
        {
            return _dbContext.Set<TModel>().Any(model => model.Id == id);
        }

        public IEnumerable<TEntity> GetAll(int pageNumber = 1, int pageSize = int.MaxValue)
        {
            var models = _dbContext.Set<TModel>()
                .Skip(--pageNumber * pageSize)
                .Take(pageSize)
                .AsEnumerable();

            return _mapper.Map<IEnumerable<TEntity>>(models);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            var model = await _dbContext.Set<TModel>()
                .FindAsync(id);
            if (model == null)
            {
                return _mapper.Map<TEntity>(model);
            }

            return _mapper.Map<TEntity>(model);
        }

        public TEntity Get(int id)
        {
            var model = _dbContext.Set<TModel>()
                .Find(id);
            if (model == null)
            {
                return _mapper.Map<TEntity>(model);
            }

            return _mapper.Map<TEntity>(model);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var model = _mapper.Map<TModel>(entity);

            await _dbContext.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
 
        public async Task<TEntity> SoftDeleteAsync(int id)
        {
            var model = await _dbContext.Set<TModel>()
                .FindAsync(id);

            //TODO Implement a true soft delete
            _dbContext.Set<TModel>()
                .Remove(model);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TEntity>(model);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var model = _mapper.Map<TModel>(entity);

            _dbContext.Update(model);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public TEntity Update(TEntity entity) {
            var model = _mapper.Map<TModel>(entity);

            _dbContext.Update(model);
            _dbContext.SaveChanges();

            return entity;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            var models = _mapper.Map<IEnumerable<TModel>>(entities);

            _dbContext.UpdateRange(models);
        }

        public async Task<IEnumerable<TEntity>> GetByExpression(
            Expression<Func<TModel, bool>> expression)
        {
            IEnumerable<TModel> entities =
                await Task.Run(() =>
                    _dbContext.Set<TModel>().Where(expression).OrderBy(entity => entity.Id).AsNoTracking());

            if (entities == null)
            {
                return Enumerable.Empty<TEntity>();
            }

            return _mapper.Map<IEnumerable<TEntity>>(entities);
        }
    }
}
