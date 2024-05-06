using Ardalis.Result;
using MediatR;
using MMMovies.OrderProcessing.Data.Repositories;
using MMMovies.OrderProcessing.Endpoints.Responses;

namespace MMMovies.OrderProcessing.UseCases;

internal class GetOrdersForUserQueryHandler(IOrderRepository orderRepository) :
  IRequestHandler<GetOrdersForUserQuery,
  Result<List<OrderSummaryResponse>>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<Result<List<OrderSummaryResponse>>> Handle(GetOrdersForUserQuery request, CancellationToken cancellationToken)
    {
        // look up UserId for EmailAddress

        // TODO: Filter by User
        var orders = await _orderRepository.ListAsync();

        var summaries = orders.Select(o => new OrderSummaryResponse
        {
            DateCreated = o.DateCreated,
            OrderId = o.Id,
            UserId = o.UserId,
            Total = o.OrderItems.Sum(oi => oi.UnitPrice) // need to .Include OrderItems
        })
          .ToList();

        return summaries;
    }
}
