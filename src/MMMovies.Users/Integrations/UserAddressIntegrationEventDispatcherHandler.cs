﻿using MediatR;
using Microsoft.Extensions.Logging;
using MMMovies.Users.Contracts;
using MMMovies.Users.Domain;

namespace MMMovies.Users.Integrations;

internal class UserAddressIntegrationEventDispatcherHandler(IMediator mediator, ILogger<UserAddressIntegrationEventDispatcherHandler> logger) : INotificationHandler<AddressAddedEvent>
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<UserAddressIntegrationEventDispatcherHandler> _logger = logger;

    public async Task Handle(AddressAddedEvent notification, CancellationToken ct)
    {
        Guid userId = Guid.Parse(notification.NewAddress.UserId);

        var addressDetails = new UserAddressDetails(userId,
          notification.NewAddress.Id,
          notification.NewAddress.StreetAddress.Street1,
          notification.NewAddress.StreetAddress.Street2,
          notification.NewAddress.StreetAddress.City,
          notification.NewAddress.StreetAddress.State,
          notification.NewAddress.StreetAddress.PostalCode,
          notification.NewAddress.StreetAddress.Country);

        await _mediator!.Publish(new NewUserAddressAddedIntegrationEvent(addressDetails), ct);

        _logger.LogInformation("[DE Handler]New address integration event sent for {user}: {address}",
          notification.NewAddress.UserId,
          notification.NewAddress.StreetAddress);
    }
}
