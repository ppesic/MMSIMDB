using System.Collections.Generic;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MMSIMDB.Application.Wrappers;
using MMSIMDB.Application.Business.Movies.ViewModel;
using MMSIMDB.Application.Interfaces.Repositories;
using System.Text.RegularExpressions;
using MMSIMDB.Domain.Entities;
using System.Linq.Expressions;
using MMSIMDB.Application.Interfaces;

namespace MMSIMDB.Application.Business.Movies.Queries.GetMoviePage
{
    public class GetMoviePageQueryHandler : IRequestHandler<GetMoviePageQueryRequest, Response<IEnumerable<MovieViewModel>>>
    {
        private readonly IMovieRepositoryAsync _movieRepositoryAsync;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IMapper _mapper;
        private const int _pageSize = 10;

        public GetMoviePageQueryHandler(IMovieRepositoryAsync movieRepositoryAsync, IAuthenticatedUserService authenticatedUserService, IMapper mapper)
        {
            _movieRepositoryAsync = movieRepositoryAsync;
            _authenticatedUserService = authenticatedUserService;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<MovieViewModel>>> Handle(GetMoviePageQueryRequest query, CancellationToken cancellationToken)
        {
            string userID = _authenticatedUserService.UserId;
            var items = await _movieRepositoryAsync.GetPageByFiltersAsync(GetFilters(query.MovieTypeID, query.Search.ToLower()), query.PageNumber, _pageSize, userID);
            //var itemsViewModel = _mapper.Map<IEnumerable<MovieViewModel>>(items);
            var itemsViewModel = items.Select(el => new MovieViewModel()
            {
                ID = el.ID,
                Description = el.Description,
                ImageName = el.ImageName,
                MovieTypeID = el.MovieTypeID,
                NumberOfRatings = el.NumberOfRatings,
                NumberOFStars = el.NumberOFStars,
                RatingSum = el.RatingSum,
                Title = el.Title,
                Year = el.Year,
                MyRating = el.MovieUserRating.FirstOrDefault()?.Rating ?? 0
            }).ToList();
            return new Response<IEnumerable<MovieViewModel>>(itemsViewModel);
        }

        private List<Expression<Func<Movie, bool>>> GetFilters(int movieTypeID, string search)
        {
            int number = 0;
            List<Expression<Func<Movie, bool>>> result = new List<Expression<Func<Movie, bool>>>
            {
                el => el.MovieTypeID == movieTypeID
            };

            if(TryToFind("at least [0-9]+ star[s]?", ref search, ref number))
            {
                result.Add(el => el.NumberOFStars >= number);
            }
            else if (TryToFind("maximum [0-9]+ star[s]?", ref search, ref number))
            {
                result.Add(el => el.NumberOFStars <= number);
            }
            if (TryToFind("[0-9]+ star[s]?", ref search, ref number))
            {
                result.Add(el => el.NumberOFStars == number);
            }
            else if (TryToFind("after [0-9]+", ref search, ref number))
            {
                result.Add(el => el.Year > number);
            }
            else if (TryToFind("before [0-9]+", ref search, ref number))
            {
                result.Add(el => el.Year < number);
            }
            else if (TryToFind("older then [0-9]+ year[s]?", ref search, ref number))
            {
                result.Add(el => el.Year < (DateTime.Now.Year - number));
            }
            else if (TryToFind("younger then [0-9]+ year[s]?", ref search, ref number))
            {
                result.Add(el => el.Year > (DateTime.Now.Year - number));
            }
            else if (TryToFind("[0-9]+ year", ref search, ref number))
            {
                result.Add(el => el.Year == number);
            }

            if (!String.IsNullOrEmpty(search))
            {
                result.Add(el => el.Title.Contains(search) || el.Description.Contains(search));
            }

            return result;
        }
        private bool TryToFind(string pattern, ref string search, ref int number) 
        {
            var match = Regex.Match(search, pattern).Value;
            if (String.IsNullOrEmpty(match))
            {
                return false;
            }
            _ = int.TryParse(Regex.Match(match, "[0-9]+").Value, out number);
            search = Regex.Replace(search, pattern, String.Empty).Trim();
            return true;
        }
    }
}
