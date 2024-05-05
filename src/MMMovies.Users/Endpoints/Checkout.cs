using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using MMMovies.Users.Endpoints.Requests;
using MMMovies.Users.Endpoints.Responses;
using MMMovies.Users.UseCases.Cart.Checkout;

namespace MMMovies.Users.Endpoints;

internal class Checkout(IMediator mediator) : Endpoint<CheckoutRequest, CheckoutResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Post("/cart/checkout");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CheckoutRequest request, CancellationToken ct = default)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var command = new CheckoutCartCommand(emailAddress!,
                                              request.ShippingAddressId,
                                              request.BillingAddressId);

        var result = await _mediator.Send(command, ct);

        if (result.Status == ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync(ct);
        }
        else
        {
            await SendOkAsync(new CheckoutResponse(result.Value), ct);
        }
    }

}
