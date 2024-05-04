using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMMovies.Movies.Data;
using MMMovies.Movies.Data.Repositories;
using MMMovies.Movies.Services;
using Serilog;
using System.Reflection;

namespace MMMovies.Movies;

public static class MovieServiceExtensions
{
    public static IServiceCollection AddMovieModuleServices(this IServiceCollection services,
                                                           ConfigurationManager config,
                                                           ILogger logger,
                                                           List<Assembly> mediatRAssemblies)
    {
        string? connectionString = config.GetConnectionString("MoviesConnectionString");
        services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IMovieService, MovieService>();

        mediatRAssemblies.Add(typeof(MovieServiceExtensions).Assembly);

        logger.Information("{Module} module services registered", "Movies");

        return services;
    }
}
