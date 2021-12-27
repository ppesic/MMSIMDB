using FluentValidation;

namespace MMSIMDB.Application.Business.Movies.Commands.AddRating
{
    public class AddRatingCommandValidator : AbstractValidator<AddRatingCommandRequest>
    {
        public AddRatingCommandValidator()
        {
            RuleFor(x => x.Rating).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(5);
        }
    }
}
