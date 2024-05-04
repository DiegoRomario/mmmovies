using FastEndpoints;
using MMMovies.Movies.Endpoints.Responses;
using MMMovies.Movies.Services;

namespace MMMovies.Movies.Endpoints;

internal class GetMovies(IMovieService movieService) : EndpointWithoutRequest<GetMoviesResponse>
{
    private readonly IMovieService _movieService = movieService;

    public override void Configure()
    {
        Get("/movies");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct = default)
    {
        var movies = await _movieService.GetMoviesAsync();

        await SendAsync(new GetMoviesResponse()
        {
            Movies = movies
        }, cancellation: ct);
    }
}