namespace MMMovies.Movies.Contracts;

public record MovieDetailsResponse(Guid MovieId, string Title, string Author, decimal Price);
