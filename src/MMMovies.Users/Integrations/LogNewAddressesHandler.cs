﻿using MediatR;
using Microsoft.Extensions.Logging;
using MMMovies.Users.Domain;

namespace MMMovies.Users.Integrations;

internal class LogNewAddressesHandler(ILogger<LogNewAddressesHandler> logger) : INotificationHandler<AddressAddedEvent>
{
    private readonly ILogger<LogNewAddressesHandler> _logger = logger;

    public Task Handle(AddressAddedEvent notification,
      CancellationToken ct)
    {
        _logger.LogInformation("[DE Handler]New address added to user {user}: {address}",
                               notification.NewAddress.UserId,
                               notification.NewAddress.StreetAddress);

        return Task.CompletedTask;
    }
}
