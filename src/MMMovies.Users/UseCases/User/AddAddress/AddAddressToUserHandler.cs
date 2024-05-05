using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using MMMovies.Users.Data.Repositories;
using MMMovies.Users.Domain;

namespace MMMovies.Users.UseCases.User.AddAddress;

internal class AddAddressToUserHandler(IApplicationUserRepository userRepository, ILogger<AddAddressToUserHandler> logger) : IRequestHandler<AddAddressToUserCommand, Result>
{
    private readonly IApplicationUserRepository _userRepository = userRepository;
    private readonly ILogger<AddAddressToUserHandler> _logger = logger;

    public async Task<Result> Handle(AddAddressToUserCommand request, CancellationToken ct)
    {
        var user = await _userRepository.GetUserWithAddressesByEmailAsync(request.EmailAddress);

        if (user is null)
        {
            return Result.Unauthorized();
        }

        var addressToAdd = new Address(request.Street1,
                                       request.Street2,
                                       request.City,
                                       request.State,
                                       request.PostalCode,
                                       request.Country);

        var userAddress = user.AddAddress(addressToAdd);
        await _userRepository.SaveChangesAsync();

        _logger.LogInformation("[UseCase] Added address {address} to user {email} (Total: {total})",
          userAddress.StreetAddress,
          request.EmailAddress,
          user.Addresses.Count);

        return Result.Success();
    }
}
