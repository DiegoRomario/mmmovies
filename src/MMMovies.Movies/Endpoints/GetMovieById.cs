using FastEndpoints;
using MMMovies.Movies.DTOs;
using MMMovies.Movies.Endpoints.Requests;
using MMMovies.Movies.Services;

namespace MMMovies.Movies.Endpoints;

internal class GetMovieById(IMovieService movieService) : Endpoint<GetMovieByIdRequest, MovieDto>
{
    private readonly IMovieService _movieService = movieService;

    public override void Configure()
    {
        Get("/movies/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetMovieByIdRequest req, CancellationToken ct)
    {
        var movie = await _movieService.GetMovieByIdAsync(req.Id);

        if (movie is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(movie, cancellation: ct);
    }
}
