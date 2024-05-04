using FastEndpoints;
using FluentValidation;

namespace MMMovies.Movies.Endpoints.Requests;

public class UpdateMoviePriceRequestValidator : Validator<UpdateMoviePriceRequest>
{
    public UpdateMoviePriceRequestValidator()
    {
        RuleFor(x => x.Id).NotNull()
                          .NotEqual(Guid.Empty)
                          .WithMessage("A movie id is required.");

        RuleFor(x => x.NewPrice).GreaterThanOrEqualTo(0)
                                .WithMessage("Movie prices may not be negative.");
    }
}
