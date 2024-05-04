using Microsoft.EntityFrameworkCore;
using MMMovies.Movies.Domain;

namespace MMMovies.Movies.Data.Repositories;

internal class MovieRepository(MovieDbContext dbContext) : IMovieRepository
{
    private readonly MovieDbContext _dbContext = dbContext;

    public async Task<Movie?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Movies.FindAsync(id);
    }
    public async Task<List<Movie>> GetAsync()
    {
        return await _dbContext.Movies.ToListAsync();
    }

    public Task AddAsync(Movie movie)
    {
        _dbContext.Add(movie);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Movie movie)
    {
        _dbContext.Remove(movie);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}