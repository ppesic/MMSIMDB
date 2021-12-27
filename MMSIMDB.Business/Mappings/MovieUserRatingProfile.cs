using AutoMapper;
using MMSIMDB.Application.Business.Movies.Commands.AddRating;
using MMSIMDB.Domain.Entities;

namespace MMSIMDB.Application.Mappings
{
    public class MovieUserRatingProfile : Profile
    {
        public MovieUserRatingProfile()
        {
            CreateMap<MovieUserRating, AddRatingCommandRequest>().ReverseMap();
        }
    }
}
