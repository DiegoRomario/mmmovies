using MediatR;
using MMMovies.OrderProcessing.Contracts;
using MMMovies.OrderProcessing.Domain;

namespace MMMovies.OrderProcessing.Integrations;
internal class PublishCreatedOrderIntegrationEventHandler(IMediator mediator) :
  INotificationHandler<OrderCreatedEvent>
{
    private readonly IMediator _mediator = mediator;

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var dto = new OrderDetailsDto()
        {
            DateCreated = notification.Order.DateCreated,
            OrderId = notification.Order.Id,
            UserId = notification.Order.UserId,
            OrderItems = notification.Order.OrderItems
          .Select(oi => new OrderItemDetails(oi.BookId,
                                             oi.Quantity,
                                             oi.UnitPrice,
                                             oi.Description))
          .ToList()
        };
        var integrationEvent = new OrderCreatedIntegrationEvent(dto);

        await _mediator.Publish(integrationEvent, cancellationToken);

    }
}
