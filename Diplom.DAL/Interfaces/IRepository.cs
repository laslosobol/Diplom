using System.Linq.Expressions;

namespace Diplom.Core.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(params object[] id);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string[] includeProperties = null,
        int? pageNumber = null, int? pageSize = null);

    Task InsertAsync(T entity);

    void Delete(T entity);
    void Update(T entity); 
}