namespace MMMovies.Reporting.Models;

public class TopMoviesByMonthReport
{
    public int Year { get; set; }
    public int Month { get; set; }
    public string MonthName { get; set; } = string.Empty;
    public List<MovieSalesResult> Results { get; set; } = [];
}
