using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Serilog;
using MMMovies.Users.Data.Repositories;
using MMMovies.Users.Data;
using MMMovies.Users.Domain;

namespace MMMovies.Users;

public static class UsersModuleServiceExtensions
{
    public static IServiceCollection AddUserModuleServices(this IServiceCollection services, ConfigurationManager config, ILogger logger, List<System.Reflection.Assembly> mediatRAssemblies)
    {
        string? connectionString = config.GetConnectionString("UsersConnectionString");
        services.AddDbContext<UsersDbContext>(config => config.UseSqlServer(connectionString));

        services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<UsersDbContext>();

        // Add User Services
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IUserStreetAddressRepository, UserStreetAddressRepository>();

        // if using MediatR in this module, add any assemblies that contain handlers to the list
        mediatRAssemblies.Add(typeof(UsersModuleServiceExtensions).Assembly);

        logger.Information("{Module} module services registered", "Users");

        return services;
    }
}
