using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MMMovies.Reporting.Models;

namespace MMMovies.Reporting.Services;

public class OrderIngestionService(IConfiguration config, ILogger<OrderIngestionService> logger)
{
    private readonly ILogger<OrderIngestionService> _logger = logger;
    private readonly string _connString = config.GetConnectionString("ReportingConnectionString")!;
    private static bool _ensureTableCreated = false;

    private async Task CreateTableAsync()
    {
        string sql = @"
                  IF NOT EXISTS (SELECT * FROM SYS.SCHEMAS WHERE NAME = 'REPORTING')
                  BEGIN
                      EXEC('CREATE SCHEMA REPORTING')
                  END

                  IF NOT EXISTS (SELECT * FROM SYS.TABLES WHERE NAME = 'MONTHLYMOVIESALES' AND TYPE = 'U')
                  BEGIN
                      CREATE TABLE REPORTING.MONTHLYMOVIESALES
                      (
                          MOVIEID UNIQUEIDENTIFIER,
                          TITLE NVARCHAR(255),
                          DIRECTOR NVARCHAR(255),
                          YEAR INT,
                          MONTH INT,
                          UNITSSOLD INT,
                          TOTALSALES DECIMAL(18, 2),
                          PRIMARY KEY (MOVIEID, YEAR, MONTH)
                      );
                  END";
        using var conn = new SqlConnection(_connString);
        _logger.LogInformation("Executing query: {sql}", sql);

        await conn.ExecuteAsync(sql);
        _ensureTableCreated = true;
    }


    public async Task AddOrUpdateMonthlyMovieSalesAsync(MovieSale sale)
    {
        if (!_ensureTableCreated) await CreateTableAsync();

        var sql = @"
                    IF EXISTS (SELECT 1 FROM REPORTING.MONTHLYMOVIESALES WHERE MOVIEID = @MOVIEID AND YEAR = @YEAR AND MONTH = @MONTH)
                    BEGIN
                        -- UPDATE EXISTING RECORD
                        UPDATE REPORTING.MONTHLYMOVIESALES
                        SET UNITSSOLD = UNITSSOLD + @UNITSSOLD, TOTALSALES = TOTALSALES + @TOTALSALES
                        WHERE MOVIEID = @MOVIEID AND YEAR = @YEAR AND MONTH = @MONTH
                    END
                    ELSE
                    BEGIN
                        -- INSERT NEW RECORD
                        INSERT INTO REPORTING.MONTHLYMOVIESALES (MOVIEID, TITLE, DIRECTOR, YEAR, MONTH, UNITSSOLD, TOTALSALES)
                        VALUES (@MOVIEID, @TITLE, @DIRECTOR, @YEAR, @MONTH, @UNITSSOLD, @TOTALSALES)
                    END";

        using var conn = new SqlConnection(_connString);
        _logger.LogInformation("Executing query: {sql}", sql);
        await conn.ExecuteAsync(sql, new
        {
            sale.MovieId,
            sale.Title,
            sale.Director,
            sale.Year,
            sale.Month,
            sale.UnitsSold,
            sale.TotalSales
        });
    }
}
