using Ardalis.Result;
using MediatR;

namespace MMMovies.Users.UseCases.User.GetById;
internal record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserDTO>>;

