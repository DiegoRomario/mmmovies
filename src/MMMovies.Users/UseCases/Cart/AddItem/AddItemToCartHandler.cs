using Ardalis.Result;
using MediatR;
using MMMovies.Movies.Contracts;
using MMMovies.Users.Data.Repositories;
using MMMovies.Users.Domain;

namespace MMMovies.Users.UseCases.Cart.AddItem;

public class AddItemToCartHandler(IApplicationUserRepository userRepository, IMediator mediator) : IRequestHandler<AddItemToCartCommand, Result>
{
    private readonly IApplicationUserRepository _userRepository = userRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken ct)
    {
        var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

        if (user is null)
        {
            return Result.Unauthorized();
        }

        var query = new MovieDetailsQuery(request.MovieId);

        var result = await _mediator.Send(query, ct);

        if (result.Status == ResultStatus.NotFound) return Result.NotFound();

        var movieDetails = result.Value;

        var description = $"{movieDetails.Title} by {movieDetails.Author}";
        var newCartItem = new CartItem(request.MovieId, description, request.Quantity, movieDetails.Price);

        user.AddItemToCart(newCartItem);

        await _userRepository.SaveChangesAsync();

        return Result.Success();
    }
}
