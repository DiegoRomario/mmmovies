using FastEndpoints;
using MMMovies.Reporting.Endpoints.Requests;
using MMMovies.Reporting.Endpoints.Responses;
using MMMovies.Reporting.Services;

namespace MMMovies.Reporting.Endpoints;

internal class TopSalesByMonth(ITopSellingMoviesReportService reportService) : Endpoint<TopSalesByMonthRequest, TopSalesByMonthResponse>
{
    private readonly ITopSellingMoviesReportService _reportService = reportService;

    public override void Configure()
    {
        Get("/topsales");
        AllowAnonymous(); // TODO: lock down
    }

    public override async Task HandleAsync(
    TopSalesByMonthRequest request,
    CancellationToken ct = default)
    {
        var report = _reportService.ReachInSqlQuery(request.Month, request.Year);
        var response = new TopSalesByMonthResponse { Report = report };
        await SendAsync(response, cancellation: ct);
    }

}



