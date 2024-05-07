using MediatR;
using Microsoft.Extensions.Logging;
using MMMovies.Movies.Contracts;
using MMMovies.OrderProcessing.Contracts;
using MMMovies.Reporting.Models;
using MMMovies.Reporting.Services;

namespace MMMovies.Reporting.Integrations;
internal class NewOrderCreatedIngestionHandler(ILogger<NewOrderCreatedIngestionHandler> logger, OrderIngestionService orderIngestionService, IMediator mediator) : INotificationHandler<OrderCreatedIntegrationEvent>
{
    private readonly ILogger<NewOrderCreatedIngestionHandler> _logger = logger;
    private readonly OrderIngestionService _orderIngestionService = orderIngestionService;
    private readonly IMediator _mediator = mediator;

    public async Task Handle(OrderCreatedIntegrationEvent notification, CancellationToken ct)
    {
        _logger.LogInformation("Handling order created event to populate reporting database...");

        var orderItems = notification.OrderDetails.OrderItems;
        int year = notification.OrderDetails.DateCreated.Year;
        int month = notification.OrderDetails.DateCreated.Month;

        foreach (var item in orderItems)
        {
            // look up movie details to get director and title
            // TODO: Implement Materialized View or other cache
            var movieDetailsQuery = new MovieDetailsQuery(item.MovieId);
            var result = await _mediator.Send(movieDetailsQuery, ct);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("Issue loading movie details for {id}", item.MovieId);
                continue;
            }

            string director = result.Value.Director;
            string title = result.Value.Title;

            var sale = new MovieSale
            {
                Director = director,
                MovieId = item.MovieId,
                Month = month,
                Title = title,
                Year = year,
                TotalSales = item.Quantity * item.UnitPrice,
                UnitsSold = item.Quantity
            };

            await _orderIngestionService.AddOrUpdateMonthlyMovieSalesAsync(sale);
        }
    }
}
