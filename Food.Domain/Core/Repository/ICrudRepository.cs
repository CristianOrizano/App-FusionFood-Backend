using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Core.Repository
{
    public interface ICrudRepository<T, ID> : IPageRepository<T>
    {
        Task<IReadOnlyList<T>> FindAllAsync();

        Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>> predicate, bool disabledTracking = true);

        Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disabledTracking = true);

        Task<T> FindByIdAsync(ID id);

        Task<T?> FindByIdAsync(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true);

        Task<T> SaveAsync(T entity);

        Task<T> DisabledByIdAsync(ID id);

    }
}
