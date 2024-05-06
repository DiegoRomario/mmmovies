﻿namespace MMMovies.OrderProcessing.Endpoints.Responses;

public class OrderSummaryResponse
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateShipped { get; set; }
    public decimal Total { get; set; }
}
