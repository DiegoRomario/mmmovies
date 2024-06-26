﻿using MediatR;

namespace MMMovies.OrderProcessing.Contracts;

public class OrderCreatedIntegrationEvent(OrderDetailsDto orderDetailsDto) : INotification
{
    public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.Now;
    public OrderDetailsDto OrderDetails { get; private set; } = orderDetailsDto;
}
