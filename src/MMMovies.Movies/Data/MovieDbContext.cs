using Microsoft.EntityFrameworkCore;
using MMMovies.Movies.Domain;
using System.Reflection;

namespace MMMovies.Movies.Data;
internal class MovieDbContext(DbContextOptions<MovieDbContext> options) : DbContext(options)
{
    internal DbSet<Movie> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Movies");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
                            .HavePrecision(18, 6);
    }
}

