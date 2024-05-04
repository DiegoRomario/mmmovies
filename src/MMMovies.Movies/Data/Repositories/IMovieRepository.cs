using MMMovies.Movies.Domain;

namespace MMMovies.Movies.Data.Repositories;

internal interface IMovieRepository
{
    Task<Movie?> GetByIdAsync(Guid id);
    Task<List<Movie>> GetAsync();
    Task AddAsync(Movie movie);
    Task DeleteAsync(Movie movie);
    Task SaveChangesAsync();
}

