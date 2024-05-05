using Ardalis.Result;
using MediatR;
using MMMovies.Users.Data.Repositories;
using MMMovies.Users.DTOs;

namespace MMMovies.Users.UseCases.User.ListAddresses;

internal class GetAddressesQueryHandler(IApplicationUserRepository userRepository) : IRequestHandler<GetAddressesQuery, Result<List<UserAddressDto>>>
{
    private readonly IApplicationUserRepository _userRepository = userRepository;

    public async Task<Result<List<UserAddressDto>>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithAddressesByEmailAsync(request.EmailAddress);

        if (user is null)
        {
            return Result.Unauthorized();
        }

        return user!.Addresses!
                    .Select(ua => new UserAddressDto(ua.Id, ua.StreetAddress.Street1,
                    ua.StreetAddress.Street2,
                    ua.StreetAddress.City,
                    ua.StreetAddress.State,
                    ua.StreetAddress.PostalCode,
                    ua.StreetAddress.Country))
                    .ToList();
    }
}
