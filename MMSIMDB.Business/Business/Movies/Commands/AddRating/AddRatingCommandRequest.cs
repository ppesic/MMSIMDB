using MediatR;
using System;
using MMSIMDB.Application.Business.Movies.ViewModel;
using MMSIMDB.Application.Wrappers;

namespace MMSIMDB.Application.Business.Movies.Commands.AddRating
{
    public partial class AddRatingCommandRequest : IRequest<Response<MovieViewModel>>
    {
        public int MovieID { get; set; }
        public int Rating { get; set; }
    }
}
