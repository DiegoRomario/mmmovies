using Ardalis.Result;
using MediatR;

namespace MMMovies.OrderProcessing.Contracts;

public record CreateOrderCommand(Guid UserId,
                                 Guid ShippingAddressId,
                                 Guid BillingAddressId,
                                 List<OrderItemDetails> OrderItems) : IRequest<Result<OrderDetailsResponse>>;
