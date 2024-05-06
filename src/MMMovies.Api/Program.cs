using FastEndpoints;
using FastEndpoints.Swagger;
using Serilog;
using System.Reflection;
using MMMovies.Movies;
using MMMovies.SharedKernel;
using FastEndpoints.Security;
using MMMovies.Users;
using MMMovies.Users.UseCases.Cart.AddItem;
using MMMovies.OrderProcessing;

var logger = Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
                                                   .WriteTo.Console()
                                                   .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddFastEndpoints()
                .AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration["Auth:JwtSecret"]!)
                .AddAuthorization()
                .SwaggerDocument();

// Add Module Services
List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.Services.AddMovieModuleServices(builder.Configuration, logger, mediatRAssemblies);
//builder.Services.AddEmailSendingModuleServices(builder.Configuration, logger, mediatRAssemblies);
//builder.Services.AddReportingModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddOrderProcessingModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddUserModuleServices(builder.Configuration, logger, mediatRAssemblies);


// Set up MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies([.. mediatRAssemblies]));
builder.Services.AddMediatRLoggingBehavior();
builder.Services.AddMediatRFluentValidationBehavior();
builder.Services.AddValidatorsFromAssemblyContaining<AddItemToCartCommandValidator>();
//Add MediatR Domain Event Dispatcher
builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

var app = builder.Build();

app.UseAuthentication()
   .UseAuthorization();

app.UseFastEndpoints()
   .UseSwaggerGen();

app.Run();
public partial class Program { } // needed for tests and mediatR 
