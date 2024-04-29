using Food.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Core.Repository
{
    public interface IPageRepository<T>
    {
        Task<PagedResult<T>> FindAllPaginatedAsync(Paging paging, Expression<Func<T, bool>> predicate);

        Task<PagedResult<T>> FindAllPaginatedAsync(Paging paging, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true);
    }
}
