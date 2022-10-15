using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Interfaces
{
    public interface IRepository<T> where T: class, IBaseEntity 
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByKeyAsync<TKey>(TKey key);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();
        Task AddRangeAsync(List<T> entities);
        IQueryable<T> Query(params Expression<Func<T, object>>[] includes);
    }
}
