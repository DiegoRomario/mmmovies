﻿using Ardalis.GuardClauses;

namespace MMMovies.OrderProcessing.Domain;

internal class OrderItem
{
    public OrderItem(Guid movieId, int quantity, decimal unitPrice, string description)
    {
        MovieId = Guard.Against.Default(movieId);
        Quantity = Guard.Against.Negative(quantity);
        UnitPrice = Guard.Against.Negative(unitPrice);
        Description = Guard.Against.NullOrEmpty(description);
    }

    private OrderItem() { } // EF

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid MovieId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string Description { get; private set; } = string.Empty;
}

