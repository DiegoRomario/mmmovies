using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using MMMovies.Users.Endpoints.Requests;
using MMMovies.Users.UseCases.User.Create;

namespace MMMovies.Users.Endpoints;

internal class Create(IMediator mediator) : Endpoint<CreateUserRequest>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Post("/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest request,
      CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(request.Email, request.Password);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            await SendResultAsync(result.ToMinimalApiResult());
            return;
        }
        await SendOkAsync(cancellationToken);
    }
}
