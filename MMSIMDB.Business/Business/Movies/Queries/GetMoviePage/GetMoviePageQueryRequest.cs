using MediatR;
using MMSIMDB.Application.Business.Movies.ViewModel;
using MMSIMDB.Application.Wrappers;

namespace MMSIMDB.Application.Business.Movies.Queries.GetMoviePage
{
    public partial class GetMoviePageQueryRequest : IRequest<Response<IEnumerable<MovieViewModel>>>
    {
        public string Search { get; set; }
        public int PageNumber { get; set; }
        public int MovieTypeID { get; set; }

    }
}
