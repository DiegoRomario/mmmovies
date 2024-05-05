using System.Security.Claims;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using MMMovies.Users.Endpoints.Requests;
using MMMovies.Users.UseCases.Cart.AddItem;

namespace MMMovies.Users.Endpoints;
internal class AddItem(IMediator mediator) : Endpoint<AddCartItemRequest>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Post("/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddCartItemRequest request, CancellationToken cancellationToken)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var command = new AddItemToCartCommand(request.MovieId, request.Quantity, emailAddress!);

        var result = await _mediator!.Send(command, cancellationToken);

        if (result.Status == ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync(cancellationToken);
        }
        if (result.Status == ResultStatus.Invalid)
        {
            await SendResultAsync(result.ToMinimalApiResult());
        }
        else
        {
            await SendOkAsync(cancellationToken);
        }
    }
}
