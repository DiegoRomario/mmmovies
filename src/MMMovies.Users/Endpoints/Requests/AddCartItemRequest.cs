namespace MMMovies.Users.Endpoints.Requests;

public record AddCartItemRequest(Guid MovieId, int Quantity);
