using Ardalis.Result;
using MediatR;
using MMMovies.Users.Contracts;
using MMMovies.Users.Data.Repositories;

namespace MMMovies.Users.Integrations;
internal class UserAddressDetailsByIdQueryHandler(IUserStreetAddressRepository addressRepo) : IRequestHandler<UserAddressDetailsByIdQuery, Result<UserAddressDetails>>
{
    private readonly IUserStreetAddressRepository _addressRepo = addressRepo;

    public async Task<Result<UserAddressDetails>> Handle(UserAddressDetailsByIdQuery request, CancellationToken ct)
    {
        var address = await _addressRepo.GetById(request.AddressId);

        if (address is null) { return Result.NotFound(); }

        Guid userId = Guid.Parse(address.UserId);

        var details = new UserAddressDetails(userId,
          address.Id,
          address.StreetAddress.Street1,
          address.StreetAddress.Street2,
          address.StreetAddress.City,
          address.StreetAddress.State,
          address.StreetAddress.PostalCode,
          address.StreetAddress.Country);

        return details;
    }

}
