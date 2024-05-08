using Ardalis.Result;
using MediatR;
using MMMovies.Users.Data.Repositories;
using MMMovies.Users.DTOs;

namespace MMMovies.Users.UseCases.User.GetById;

internal class GetUserByIdHandler(IApplicationUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, Result<UserDTO>>
{
    private readonly IApplicationUserRepository _userRepository = userRepository;

    public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.UserId);

        if (user is null)
        {
            return Result.NotFound();
        }

        return new UserDTO(Guid.Parse(user!.Id), user.Email!);
    }
}

