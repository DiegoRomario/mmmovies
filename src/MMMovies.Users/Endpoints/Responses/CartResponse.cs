using MMMovies.Users.DTOs;

namespace MMMovies.Users.Endpoints.Responses;

public class CartResponse
{
    public List<CartItemDto> CartItems { get; set; } = [];
}
