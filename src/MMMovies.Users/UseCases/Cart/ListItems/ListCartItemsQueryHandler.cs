using Ardalis.Result;
using MediatR;
using MMMovies.Users.Data.Repositories;
using MMMovies.Users.DTOs;

namespace MMMovies.Users.UseCases.Cart.ListItems;

internal class GetCartItemsQueryHandler(IApplicationUserRepository userRepository) : IRequestHandler<GetCartItemsQuery, Result<List<CartItemDto>>>
{
    private readonly IApplicationUserRepository _userRepository = userRepository;

    public async Task<Result<List<CartItemDto>>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

        if (user is null)
        {
            return Result.Unauthorized();
        }

        return user.CartItems.Select(item => new CartItemDto(item.Id, item.MovieId, item.Description, item.Quantity, item.UnitPrice))
                             .ToList();
    }
}
