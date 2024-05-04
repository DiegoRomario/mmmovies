using MMMovies.Movies.DTOs;

namespace MMMovies.Movies.Services;
internal interface IMovieService
{
    Task<List<MovieDto>> GetMoviesAsync();
    Task<MovieDto> GetMovieByIdAsync(Guid id);
    Task CreateMovieAsync(MovieDto newMovie);
    Task DeleteMovieAsync(Guid id);
    Task UpdateMoviePriceAsync(Guid MovieId, decimal newPrice);
}