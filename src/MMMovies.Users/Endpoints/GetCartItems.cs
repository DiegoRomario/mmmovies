using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using MMMovies.Users.Endpoints.Responses;
using MMMovies.Users.UseCases.Cart.ListItems;

namespace MMMovies.Users.Endpoints;

internal class GetCartItems(IMediator mediator) : EndpointWithoutRequest<CartResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var query = new GetCartItemsQuery(emailAddress!);

        var result = await _mediator.Send(query, ct);

        if (result.Status == ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync(ct);
        }
        else
        {
            var response = new CartResponse()
            {
                CartItems = result.Value
            };
            await SendAsync(response, cancellation: ct);
        }
    }
}
