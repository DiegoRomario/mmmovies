using Ardalis.Result;
using MediatR;
using MMMovies.Users.Contracts;
using MMMovies.Users.UseCases.User.GetById;

namespace MMMovies.Users.Integrations;

internal class UserDetailsByIdHandler : IRequestHandler<UserDetailsByIdQuery, Result<UserDetails>>
{
    private readonly IMediator _mediator;

    public UserDetailsByIdHandler(IMediator mediator) => _mediator = mediator;

    public async Task<Result<UserDetails>> Handle(UserDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(request.UserId);

        var result = await _mediator.Send(query, cancellationToken);

        return result.Map(u => new UserDetails(u.UserId, u.EmailAddress));
    }
}
