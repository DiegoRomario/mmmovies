using FastEndpoints;
using FluentValidation;

namespace MMMovies.Movies.Endpoints.Requests;

public class CreateMovieRequestValidator : Validator<CreateMovieRequest>
{
    public CreateMovieRequestValidator()
    {
        RuleFor(x => x.Title).NotNull()
                             .NotEmpty()
                             .WithMessage("A movie title is required.");

        RuleFor(x => x.Director).NotNull()
                                .NotEmpty()
                                .WithMessage("A movie director is required.");

        RuleFor(x => x.Price).GreaterThanOrEqualTo(0m)
                             .WithMessage("Movie prices must be positive values.");
    }
}
