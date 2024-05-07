using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MMMovies.Reporting.Models;
using System.Globalization;

namespace MMMovies.Reporting.Services;

internal class TopSellingMoviesReportService(IConfiguration config, ILogger<TopSellingMoviesReportService> logger) : ITopSellingMoviesReportService
{
    private readonly ILogger<TopSellingMoviesReportService> _logger = logger;
    private readonly string _connString = config.GetConnectionString("OrderProcessingConnectionString")!;

    public TopMoviesByMonthReport ReachInSqlQuery(int month, int year)
    {
        string sql = @"
                    SELECT B.ID, B.TITLE, B.DIRECTOR, SUM(OI.QUANTITY) AS UNITS, SUM(OI.UNITPRICE * OI.QUANTITY) AS SALES
                    FROM MOVIES.MOVIES B 
                    INNER JOIN ORDERPROCESSING.ORDERITEM OI ON B.ID = OI.MOVIEID
                    INNER JOIN ORDERPROCESSING.ORDERS O ON O.ID = OI.ORDERID
                    WHERE MONTH(O.DATECREATED) = @MONTH AND YEAR(O.DATECREATED) = @YEAR
                    GROUP BY B.ID, B.TITLE, B.DIRECTOR
                    ORDER BY SALES DESC";

        using var conn = new SqlConnection(_connString);
        _logger.LogInformation("Executing query: {sql}", sql);
        var results = conn.Query<MovieSalesResult>(sql, new { month, year })
          .ToList();

        var report = new TopMoviesByMonthReport
        {
            Year = year,
            Month = month,
            MonthName = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(month),
            Results = results
        };

        return report;
    }
}
