using MMSIMDB.Domain.Entities;
using MMSIMDB.Application.Interfaces.Repositories;
using MMSIMDB.Persistence.Contexts;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MMSIMDB.Persistence.Repositories
{
    public class MovieRepositoryAsync : GenericRepositoryAsync<Movie>, IMovieRepositoryAsync
    {
        public MovieRepositoryAsync(MMSIMDBDBContext context) : base(context)
        {
        }

        public async Task<List<Movie>> GetPageByFiltersAsync(List<Expression<Func<Movie, bool>>> filters, int pageNumber, int pageSize, string userID)
        {
            var query = GetAll().Include(el => el.MovieUserRating.Where(s => s.UserID == userID)).AsQueryable<Movie>();
            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }
            return await query.OrderByDescending(el => el.NumberOFStars).ThenBy(el => el.ID).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
        }
    }
}