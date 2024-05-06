namespace MMMovies.OrderProcessing.Contracts;

public record OrderItemDetails(Guid MovieId,
                               int Quantity,
                               decimal UnitPrice,
                               string Description);
