using Food.Domain.Core.Models;
using Food.Domain.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Food.Infraestructura.Core.Persistences
{
    public abstract class CrudRepository<T, ID, Context> : ICrudRepository<T, ID>, IPageRepository<T> where Context : DbContext where T : class
    {
        private readonly Context _context;

        private readonly DbSet<T> _table;

        protected CrudRepository(Context context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public virtual async Task<T> DisabledByIdAsync(ID id)
        {
            
            return null;
        }

        public virtual async Task<IReadOnlyList<T>> FindAllAsync()
        {
            return await _table.AsNoTracking().ToListAsync();
        }

        public virtual async Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>> predicate, bool disabledTracking = true)
        {
            IQueryable<T> query = _table.Where(predicate);
            if (disabledTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disabledTracking = true)
        {
            IQueryable<T> query = _table;
            if (disabledTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (IQueryable<T> current, Expression<Func<T, object>> include) => current.Include(include));
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<PagedResult<T>> FindAllPaginatedAsync(Paging paging, Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _table.Where(predicate);
            int totalElements = await query.CountAsync();
            int skip = (paging.PageNumber - 1) * paging.PageSize;
            return new PagedResult<T>(await query.Skip(skip).Take(paging.PageSize).ToListAsync(), paging, totalElements);
        }

        public virtual async Task<PagedResult<T>> FindAllPaginatedAsync(Paging paging, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _table;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (IQueryable<T> current, Expression<Func<T, object>> include) => current.Include(include));
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy!(query).AsQueryable();
            }

            int totalElements = await query.CountAsync();
            int skip = (paging.PageNumber - 1) * paging.PageSize;
            return new PagedResult<T>(await query.Skip(skip).Take(paging.PageSize).ToListAsync(), paging, totalElements);
        }

        public virtual async Task<T> FindByIdAsync(ID id)
        {
            return await _table.FindAsync(id);
        }

        public virtual async Task<T?> FindByIdAsync(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _table;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (IQueryable<T> current, Expression<Func<T, object>> include) => current.Include(include));
            }

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<T> SaveAsync(T entity)
        {
            switch (_context.Entry(entity).State)
            {
                case EntityState.Detached:
                    _context.Set<T>().Add(entity);
                    break;
                case EntityState.Modified:
                    _context.Set<T>().Update(entity);
                    break;
                default:
                    _context.Entry(entity).State = EntityState.Modified;
                    break;
            }

            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
