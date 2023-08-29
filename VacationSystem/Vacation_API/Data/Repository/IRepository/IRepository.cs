using System.Linq.Expressions;

namespace Vacation_API.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>? filter = null, string? includeProperties = null);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        Task<T> CreateAsync(T entity);
    }
}
