using Ardalis.Result;
using MediatR;
using MMMovies.Users.DTOs;

namespace MMMovies.Users.UseCases.User.GetById;
internal record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserDTO>>;

