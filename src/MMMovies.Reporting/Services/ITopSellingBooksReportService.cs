using MMMovies.Reporting.Models;

namespace MMMovies.Reporting.Services;

internal interface ITopSellingMoviesReportService
{
    TopMoviesByMonthReport ReachInSqlQuery(int month, int year);
}
