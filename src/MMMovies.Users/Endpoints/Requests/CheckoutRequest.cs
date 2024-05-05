namespace MMMovies.Users.Endpoints.Requests;

public record CheckoutRequest(Guid ShippingAddressId, Guid BillingAddressId);
