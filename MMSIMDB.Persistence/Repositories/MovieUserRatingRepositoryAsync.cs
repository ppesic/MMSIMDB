using MMSIMDB.Domain.Entities;
using MMSIMDB.Application.Interfaces.Repositories;
using MMSIMDB.Persistence.Contexts;

namespace MMSIMDB.Persistence.Repositories
{
    public class MovieUserRatingRepositoryAsync : GenericRepositoryAsync<MovieUserRating>, IMovieUserRatingRepositoryAsync
    {
        public MovieUserRatingRepositoryAsync(MMSIMDBDBContext context) : base(context)
        {
        }
    }
}