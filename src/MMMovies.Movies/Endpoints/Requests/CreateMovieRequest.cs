namespace MMMovies.Movies.Endpoints.Requests;

public record CreateMovieRequest(Guid? Id, string Title, string Director, decimal Price);
