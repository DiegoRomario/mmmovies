using MMMovies.Reporting.Models;

namespace MMMovies.Reporting.Endpoints.Responses;

public class TopSalesByMonthResponse
{
    public TopMoviesByMonthReport Report { get; set; } = default!;
}



