using FastEndpoints;
using MMMovies.Movies.Endpoints.Requests;
using MMMovies.Movies.Services;

namespace MMMovies.Movies.Endpoints;

internal class DeleteMovie(IMovieService movieService) : Endpoint<DeleteMovieRequest>
{
    private readonly IMovieService _movieService = movieService;

    public override void Configure()
    {
        Delete("/movies/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteMovieRequest request, CancellationToken ct)
    {
        // TODO: Implement NotFound

        await _movieService.DeleteMovieAsync(request.Id);

        await SendNoContentAsync(ct);
    }
}