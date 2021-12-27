using MMSIMDB.Domain.Entities;
using MMSIMDB.Application.Interfaces.Repositories;
using MMSIMDB.Persistence.Contexts;

namespace MMSIMDB.Persistence.Repositories
{
    public class MovieTypeRepositoryAsync : GenericRepositoryAsync<MovieType>, IMovieTypeRepositoryAsync
    {
        public MovieTypeRepositoryAsync(MMSIMDBDBContext context) : base(context)
        {
        }
    }
}