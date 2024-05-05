using Ardalis.Result;
using MediatR;

namespace MMMovies.Users.Contracts;

public record UserAddressDetailsByIdQuery(Guid AddressId) : IRequest<Result<UserAddressDetails>>;
