using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Serilog;
using MMMovies.OrderProcessing.Data;
using System.Reflection;
using MMMovies.OrderProcessing.Data.Repositories;
using MMMovies.OrderProcessing.Data.Cache;

namespace MMMovies.OrderProcessing;

public static class OrderProcessingModuleServiceExtensions
{
    public static IServiceCollection AddOrderProcessingModuleServices(this IServiceCollection services,
                                                                      ConfigurationManager config,
                                                                      ILogger logger,
                                                                      List<Assembly> mediatRAssemblies)
    {
        string? connectionString = config.GetConnectionString("OrderProcessingConnectionString");
        services.AddDbContext<OrderProcessingDbContext>(config =>
          config.UseSqlServer(connectionString));

        // Add Services
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<RedisOrderAddressCache>();
        services.AddScoped<IOrderAddressCache, ReadThroughOrderAddressCache>();

        // if using MediatR in this module, add any assemblies that contain handlers to the list
        mediatRAssemblies.Add(typeof(OrderProcessingModuleServiceExtensions).Assembly);

        logger.Information("{Module} module services registered", "OrderProcessing");

        return services;
    }
}
