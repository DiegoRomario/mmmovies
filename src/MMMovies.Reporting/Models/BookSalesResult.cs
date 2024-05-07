namespace MMMovies.Reporting.Models;

public record MovieSalesResult(Guid MovieId,
                              string Title,
                              string Director,
                              int Units,
                              decimal Sales)
{
    private MovieSalesResult() : this(default!, default!, default!, default!, default!) { }
}


