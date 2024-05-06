using Ardalis.Result;
using MediatR;
using MMMovies.OrderProcessing.Contracts;
using MMMovies.Users.Data.Repositories;

namespace MMMovies.Users.UseCases.Cart.Checkout;

internal class CheckoutCartHandler(IApplicationUserRepository userRepository, IMediator mediator) : IRequestHandler<CheckoutCartCommand, Result<Guid>>
{
    private readonly IApplicationUserRepository _userRepository = userRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<Result<Guid>> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

        if (user is null)
        {
            return Result.Unauthorized();
        }

        var items = user.CartItems.Select(item => new OrderItemDetails(item.MovieId,
                                                                       item.Quantity,
                                                                       item.UnitPrice,
                                                                       item.Description)).ToList();

        var createOrderCommand = new CreateOrderCommand(Guid.Parse(user.Id),
          request.ShippingAddressId,
          request.BillingAddressId,
          items);

        // TODO: Consider replacing with a message-based approach for perf reasons
        var result = await _mediator.Send(createOrderCommand); // synchronous

        if (!result.IsSuccess)
        {
            // Change from a Result<OrderDetailsResponse> to Result<Guid>
            return result.Map(x => x.OrderId);
        }

        user.ClearCart();
        await _userRepository.SaveChangesAsync();

        return Result.Success(result.Value.OrderId);
    }
}
