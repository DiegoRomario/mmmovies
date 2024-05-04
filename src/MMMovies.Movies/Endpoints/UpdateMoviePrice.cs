using FastEndpoints;
using MMMovies.Movies.DTOs;
using MMMovies.Movies.Endpoints.Requests;
using MMMovies.Movies.Services;

namespace MMMovies.Movies.Endpoints;

internal class UpdateMoviePrice(IMovieService movieService) : Endpoint<UpdateMoviePriceRequest, MovieDto>
{
    private readonly IMovieService _movieService = movieService;

    public override void Configure()
    {
        Put("/movies/{Id}/pricehistory");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateMoviePriceRequest request, CancellationToken ct)
    {
        // TODO: Handle not found
        await _movieService.UpdateMoviePriceAsync(request.Id, request.NewPrice);
        await SendNoContentAsync(cancellation: ct);
    }
}
