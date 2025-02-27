using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AKUTRescue.Core.Repositories;
using Microsoft.EntityFrameworkCore.Query;

public interface IAsyncRepository<T, TId> where T : Entity<TId>
{
    // Tekil sorgu metodları
    Task<T> GetByIdAsync(TId id);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    // Liste sorgu metodları
    Task<IPaginate<T>> GetListAsync(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        int index = 0,
        int size = 10,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    Task<IPaginate<T>> GetListByPaginationAsync(
        PaginationParams pagination,
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    // Kontrol metodları
    Task<bool> AnyAsync(
        Expression<Func<T, bool>> predicate,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    // Ekleme metodları
    Task<T> AddAsync(T entity);
    Task<ICollection<T>> AddRangeAsync(ICollection<T> entities);

    // Güncelleme metodları
    Task<T> UpdateAsync(T entity);
    Task<ICollection<T>> UpdateRangeAsync(ICollection<T> entities);

    // Silme metodları
    Task<T> DeleteAsync(T entity);
    Task<ICollection<T>> DeleteRangeAsync(ICollection<T> entities);
    Task<T> DeleteByIdAsync(TId id);

    // Sayım metodları
    Task<int> CountAsync(
        Expression<Func<T, bool>> predicate = null,
        CancellationToken cancellationToken = default);
} 