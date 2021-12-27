using AutoMapper;
using MediatR;
using MMSIMDB.Application.Business.Movies.ViewModel;
using MMSIMDB.Application.Interfaces;
using MMSIMDB.Application.Interfaces.Repositories;
using MMSIMDB.Application.Wrappers;
using MMSIMDB.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace MMSIMDB.Application.Business.Movies.Commands.AddRating
{
    public class AddRatingCommandHandler : IRequestHandler<AddRatingCommandRequest, Response<MovieViewModel>>
    {
        private readonly IMovieUserRatingRepositoryAsync _movieUserRatingRepositoryAsync;
        private readonly IMovieRepositoryAsync _movieRepositoryAsync;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IMapper _mapper;
        public AddRatingCommandHandler(
            IMovieUserRatingRepositoryAsync movieUserRatingRepositoryAsync, 
            IMovieRepositoryAsync movieRepositoryAsync, 
            IAuthenticatedUserService authenticatedUserService, 
            IMapper mapper)
        {
            _movieUserRatingRepositoryAsync = movieUserRatingRepositoryAsync;
            _movieRepositoryAsync = movieRepositoryAsync;
            _authenticatedUserService = authenticatedUserService;
            _mapper = mapper;
        }

        public async Task<Response<MovieViewModel>> Handle(AddRatingCommandRequest request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<MovieUserRating>(request);
            item.UserID = _authenticatedUserService.UserId;
            var itemViewModel = new MovieViewModel();
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var movie = await _movieRepositoryAsync.GetByIDAsync(request.MovieID);
                    if (movie == null) throw new Exception("Movie is not found");

                    var rating = _movieUserRatingRepositoryAsync.GetAllByFilter(el => el.MovieID == request.MovieID && el.UserID == item.UserID).FirstOrDefault();
                    if (rating == null && request.Rating != 0)
                    {
                        movie.RatingSum += request.Rating;
                        movie.NumberOfRatings += 1;
                        movie.NumberOFStars = (int)Math.Round((decimal)movie.RatingSum / movie.NumberOfRatings);
                        await _movieRepositoryAsync.UpdateAsync(movie);
                        await _movieUserRatingRepositoryAsync.AddAsync(item);
                    }
                    else if (rating != null && request.Rating != 0 && rating.Rating != request.Rating)
                    {
                        movie.RatingSum += request.Rating - rating.Rating;
                        movie.NumberOFStars = (int)Math.Round((decimal)movie.RatingSum / movie.NumberOfRatings);
                        rating.Rating = request.Rating;
                        await _movieRepositoryAsync.UpdateAsync(movie);
                        await _movieUserRatingRepositoryAsync.UpdateAsync(rating);
                    }
                    else if (rating != null && request.Rating == 0)
                    {
                        movie.RatingSum -= rating.Rating;
                        movie.NumberOfRatings -= 1;
                        movie.NumberOFStars = movie.NumberOfRatings == 0 ? 0 : (int)Math.Round((decimal)movie.RatingSum / movie.NumberOfRatings);
                        await _movieRepositoryAsync.UpdateAsync(movie);
                        await _movieUserRatingRepositoryAsync.DeleteAsync(rating);
                    }
                    transactionScope.Complete();
                    itemViewModel = _mapper.Map<MovieViewModel>(movie);
                    itemViewModel.MyRating = request.Rating;
                }
                catch (Exception ex)
                {
                    transactionScope.Dispose();
                }
            }
            return new Response<MovieViewModel>(itemViewModel);
        }
    }
}
