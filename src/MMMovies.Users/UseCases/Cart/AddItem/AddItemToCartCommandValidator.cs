using FluentValidation;

namespace MMMovies.Users.UseCases.Cart.AddItem;

public class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
{
    public AddItemToCartCommandValidator()
    {
        RuleFor(x => x.EmailAddress).NotEmpty()
                                    .WithMessage("EmailAddress is required.");

        RuleFor(x => x.Quantity).GreaterThan(0)
                                .WithMessage("Quantity must be at least 1.");

        RuleFor(x => x.MovieId).NotEmpty()
                              .WithMessage("Not a valid MovieId.");
    }
}
