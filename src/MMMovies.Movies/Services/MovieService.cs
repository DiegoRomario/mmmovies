using MMMovies.Movies.Data.Repositories;
using MMMovies.Movies.Domain;
using MMMovies.Movies.DTOs;

namespace MMMovies.Movies.Services;
internal class MovieService(IMovieRepository movieRepository) : IMovieService
{
    private readonly IMovieRepository _movieRepository = movieRepository;

    public async Task CreateMovieAsync(MovieDto movieDTO)
    {
        var movie = new Movie(movieDTO.Id, movieDTO.Title, movieDTO.Director, movieDTO.Price);

        await _movieRepository.AddAsync(movie);
        await _movieRepository.SaveChangesAsync();
    }

    public async Task DeleteMovieAsync(Guid id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);

        if (movie is not null)
        {
            await _movieRepository.DeleteAsync(movie);
            await _movieRepository.SaveChangesAsync();
        }
    }

    public async Task<MovieDto> GetMovieByIdAsync(Guid id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);

        // TODO: handle not found case

        return new MovieDto(movie!.Id, movie.Title, movie.Director, movie.Price);
    }

    public async Task<List<MovieDto>> GetMoviesAsync()
    {
        var movies = (await _movieRepository.GetAsync())
                     .Select(movie => new MovieDto(movie.Id, movie.Title, movie.Director, movie.Price))
                     .ToList();

        // TODO: add mappers
        return movies;
    }

    public async Task UpdateMoviePriceAsync(Guid movieId, decimal newPrice)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);

        // TODO: handle not found case

        movie.UpdatePrice(newPrice);
        await _movieRepository.SaveChangesAsync();
    }
}