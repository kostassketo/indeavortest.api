using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Indeavor.Test.Abstractions.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);

        Task<EntityEntry<T>> AddOrUpdateAsync(T entity);

        Task<EntityEntry<T>> RemoveAsync(T entity);
    }
}
