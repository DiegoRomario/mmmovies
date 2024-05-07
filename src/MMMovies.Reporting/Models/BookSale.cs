namespace MMMovies.Reporting.Models;

public class MovieSale
{
    public Guid MovieId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Month { get; set; }
    public int UnitsSold { get; set; }
    public decimal TotalSales { get; set; }
}

