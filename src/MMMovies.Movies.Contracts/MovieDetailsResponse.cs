namespace MMMovies.Movies.Contracts;

public record MovieDetailsResponse(Guid MovieId, string Title, string Director, decimal Price);
