using Ardalis.Result;
using System.Security.Claims;
using FastEndpoints;
using MediatR;
using MMMovies.Users.Endpoints.Requests;
using MMMovies.Users.UseCases.User.AddAddress;

namespace MMMovies.Users.Endpoints;

internal sealed class AddAddress(IMediator mediator) : Endpoint<AddAddressRequest>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Post("/users/addresses");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddAddressRequest request, CancellationToken cancellationToken = default)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var command = new AddAddressToUserCommand(emailAddress!,
          request.Street1,
          request.Street2,
          request.City,
          request.State,
          request.PostalCode,
          request.Country);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Status == ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync(cancellationToken);
        }
        else
        {
            await SendOkAsync(cancellationToken);
        }
    }
}
