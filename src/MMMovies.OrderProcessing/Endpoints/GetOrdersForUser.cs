using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using MMMovies.OrderProcessing.Endpoints.Responses;
using MMMovies.OrderProcessing.UseCases;

namespace MMMovies.OrderProcessing.Endpoints;

internal class GetOrdersForUser(IMediator mediator) : EndpointWithoutRequest<GetOrdersForUserResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("/orders");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var query = new GetOrdersForUserQuery(emailAddress!);

        var result = await _mediator.Send(query, ct);

        if (result.Status == ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync(ct);
        }
        else
        {
            var response = new GetOrdersForUserResponse
            {
                Orders = result.Value.Select(o => new OrderSummaryResponse
                {
                    DateCreated = o.DateCreated,
                    DateShipped = o.DateShipped,
                    Total = o.Total,
                    UserId = o.UserId,
                    OrderId = o.OrderId
                }).ToList()
            };
            await SendAsync(response, cancellation: ct);
        }
    }
}


