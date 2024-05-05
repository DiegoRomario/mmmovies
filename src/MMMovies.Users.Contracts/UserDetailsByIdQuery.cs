using Ardalis.Result;
using MediatR;

namespace MMMovies.Users.Contracts;

public record UserDetailsByIdQuery(Guid UserId) : IRequest<Result<UserDetails>>;
