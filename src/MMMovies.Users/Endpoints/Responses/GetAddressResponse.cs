using MMMovies.Users.DTOs;

namespace MMMovies.Users.Endpoints.Responses;

public class GetAddressResponse
{
    public List<UserAddressDto> Addresses { get; set; } = [];
}
