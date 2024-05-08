using MediatR;
using Microsoft.Extensions.Logging;
using MMMovies.OrderProcessing.Data.Cache;
using MMMovies.OrderProcessing.Data.Repositories;
using MMMovies.OrderProcessing.Domain;
using MMMovies.Users.Contracts;

namespace MMMovies.OrderProcessing.Integrations;

internal class AddressCacheUpdatingNewUserAddressHandler(IOrderAddressCache addressCache, ILogger<AddressCacheUpdatingNewUserAddressHandler> logger) : INotificationHandler<NewUserAddressAddedIntegrationEvent>
{
    private readonly IOrderAddressCache _addressCache = addressCache;
    private readonly ILogger<AddressCacheUpdatingNewUserAddressHandler> _logger = logger;

    public async Task Handle(NewUserAddressAddedIntegrationEvent notification,
      CancellationToken ct)
    {
        var orderAddress = new OrderAddress(notification.Details.AddressId,
          new Address(notification.Details.Street1,
            notification.Details.Street2,
            notification.Details.City,
            notification.Details.State,
            notification.Details.PostalCode,
            notification.Details.Country));

        await _addressCache.StoreAsync(orderAddress);

        _logger.LogInformation("Cache updated with new address {address}", orderAddress);
    }
}

