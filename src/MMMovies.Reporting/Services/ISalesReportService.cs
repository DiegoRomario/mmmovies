using MMMovies.Reporting.Models;

namespace MMMovies.Reporting.Services;

internal interface ISalesReportService
{
    Task<TopMoviesByMonthReport> GetTopMoviesByMonthReportAsync(int month, int year);
}

