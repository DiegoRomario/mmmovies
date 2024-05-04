using MMMovies.Movies.DTOs;

namespace MMMovies.Movies.Endpoints.Responses;

public class GetMoviesResponse
{
    public List<MovieDto> Movies { get; set; } = [];
}

