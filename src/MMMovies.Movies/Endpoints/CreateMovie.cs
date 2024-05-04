using FastEndpoints;
using MMMovies.Movies.DTOs;
using MMMovies.Movies.Endpoints.Requests;
using MMMovies.Movies.Services;

namespace MMMovies.Movies.Endpoints;

internal class CreateMovie(IMovieService movieService) : Endpoint<CreateMovieRequest, MovieDto>
{
    private readonly IMovieService _movieService = movieService;

    public override void Configure()
    {
        Post("/movies");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateMovieRequest request, CancellationToken ct)
    {
        var newMovieDto = new MovieDto(request.Id ?? Guid.NewGuid(),
          request.Title,
          request.Director,
          request.Price);

        await _movieService.CreateMovieAsync(newMovieDto);

        await SendCreatedAtAsync<GetMovieById>(new { newMovieDto.Id }, newMovieDto, cancellation: ct);
    }
}