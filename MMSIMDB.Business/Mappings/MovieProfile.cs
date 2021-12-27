using AutoMapper;
using MMSIMDB.Application.Business.Movies.ViewModel;
using MMSIMDB.Domain.Entities;

namespace MMSIMDB.Application.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieViewModel>().ReverseMap();
        }
    }
}
