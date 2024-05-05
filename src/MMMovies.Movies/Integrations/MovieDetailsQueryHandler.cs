using Ardalis.Result;
using MediatR;
using MMMovies.Movies.Contracts;
using MMMovies.Movies.Services;

namespace MMMovies.Movies.Integrations;

internal class MovieDetailsQueryHandler(IMovieService movieService) : IRequestHandler<MovieDetailsQuery, Result<MovieDetailsResponse>>
{
    private readonly IMovieService _movieService = movieService;

    public async Task<Result<MovieDetailsResponse>> Handle(MovieDetailsQuery request, CancellationToken cancellationToken)
    {
        var movie = await _movieService.GetMovieByIdAsync(request.movieId);

        if (movie is null) return Result.NotFound();

        var response = new MovieDetailsResponse(movie.Id, movie.Title, movie.Director, movie.Price);

        return response;
    }
}
