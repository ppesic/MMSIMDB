using MMSIMDB.Domain.Common;
using System.Linq.Expressions;

namespace MMSIMDB.Application.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : BaseEntity
    {
        Task<T> GetByIDAsync(int id, string include = "");
        Task<IReadOnlyList<T>> GetAllAsync(string include = "");
        Task<T> AddAsync(T entity);
        Task<T[]> AddAsync(T[] entities);
        Task UpdateAsync(T entity);
        Task UpdateAsync(T entity, object newValues);
        Task DeleteAsync(T entity);
        Task DeleteAsync(T[] entities);
        Task<IReadOnlyList<T>> GetByFilterAsync(Expression<Func<T, bool>> filter, string include = "");
        System.Linq.IQueryable<T> GetAll(string include = "");
        System.Linq.IQueryable<T> GetAllByFilter(Expression<Func<T, bool>> filter, string include = "");
    }
}
