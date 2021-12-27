using MMSIMDB.Domain.Entities;
using System.Linq.Expressions;

namespace MMSIMDB.Application.Interfaces.Repositories
{
    public interface IMovieRepositoryAsync : IGenericRepositoryAsync<Movie>
    {
        Task<List<Movie>> GetPageByFiltersAsync(List<Expression<Func<Movie, bool>>> list, int pageNumber, int pageSize, string userID);
    }
}