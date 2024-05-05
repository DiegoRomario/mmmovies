using Ardalis.Result;
using System.Security.Claims;
using FastEndpoints;
using MediatR;
using MMMovies.Users.Endpoints.Responses;
using MMMovies.Users.UseCases.User.ListAddresses;

namespace MMMovies.Users.Endpoints;

internal class GetAddresses(IMediator mediator) : EndpointWithoutRequest<GetAddressResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("/users/addresses");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct = default)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var query = new GetAddressesQuery(emailAddress!);

        var result = await _mediator.Send(query, ct);

        if (result.Status == ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync(ct);
        }
        else
        {
            var response = new GetAddressResponse
            {
                Addresses = result.Value
            };

            await SendAsync(response, cancellation: ct);
        }
    }
}
