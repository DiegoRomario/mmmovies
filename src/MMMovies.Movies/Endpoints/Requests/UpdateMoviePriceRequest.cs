namespace MMMovies.Movies.Endpoints.Requests;

public record UpdateMoviePriceRequest(Guid Id, decimal NewPrice);