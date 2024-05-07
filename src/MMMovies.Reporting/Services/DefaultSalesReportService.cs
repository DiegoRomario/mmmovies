using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MMMovies.Reporting.Models;
using System.Globalization;

namespace MMMovies.Reporting.Services;

internal class DefaultSalesReportService(IConfiguration config, ILogger<DefaultSalesReportService> logger) : ISalesReportService
{
    private readonly ILogger<DefaultSalesReportService> _logger = logger;
    private readonly string _connString = config.GetConnectionString("ReportingConnectionString")!;

    public async Task<TopMoviesByMonthReport> GetTopMoviesByMonthReportAsync(int month, int year)
    {
        string sql = @"
                    SELECT MOVIEID, TITLE, DIRECTOR, UNITSSOLD AS UNITS, TOTALSALES AS SALES
                    FROM REPORTING.MONTHLYMOVIESALES
                    WHERE MONTH = @MONTH AND YEAR = @YEAR
                    ORDER BY TOTALSALES DESC";

        using var conn = new SqlConnection(_connString);
        _logger.LogInformation("Executing query: {sql}", sql);
        var results = (await conn.QueryAsync<MovieSalesResult>(sql, new { month, year }))
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

