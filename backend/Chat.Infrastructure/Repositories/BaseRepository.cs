using Chat.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        protected readonly ChatDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _dbContext.AddRangeAsync(entities);
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => _dbSet.Remove(entity));
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => _dbSet.RemoveRange(entities));
        }

        public async Task<T> GetByKeyAsync<TKey>(TKey key)
        {
            return await _dbSet.FindAsync(key);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => _dbContext.Entry(entity).State = EntityState.Modified);
        }

        public IQueryable<T> Query(params Expression<Func<T, object>>[] includes)
        {
            var query = includes
                .Aggregate<Expression<Func<T, object>>, IQueryable<T>>(_dbSet, (current, include) => current.Include(include));

            return query;
        }
    }
}
