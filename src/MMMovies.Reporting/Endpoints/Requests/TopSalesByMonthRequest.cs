using Microsoft.AspNetCore.Mvc;

namespace MMMovies.Reporting.Endpoints.Requests;

internal class TopSalesByMonthRequest
{
    [FromQuery]
    public int Month { get; set; }
    [FromQuery]
    public int Year { get; set; }
}



