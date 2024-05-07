using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Reflection;
using MMMovies.Reporting.Services;

namespace MMMovies.Reporting;

public static class ReportingModuleServicesExtensions
{
    public static IServiceCollection AddReportingModuleServices(this IServiceCollection services, ConfigurationManager config, ILogger logger, List<Assembly> mediatRAssemblies)
    {
        // configure module services
        services.AddScoped<ITopSellingMoviesReportService, TopSellingMoviesReportService>();
        services.AddScoped<ISalesReportService, DefaultSalesReportService>();
        services.AddScoped<OrderIngestionService>();

        // if using MediatR in this module, add any assemblies that contain handlers to the list
        mediatRAssemblies.Add(typeof(ReportingModuleServicesExtensions).Assembly);

        logger.Information("{Module} module services registered", "Reporting");
        return services;
    }
}

