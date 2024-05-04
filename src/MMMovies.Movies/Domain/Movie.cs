using Ardalis.GuardClauses;

namespace MMMovies.Movies.Domain;
internal class Movie
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public string Director { get; private set; }
    public decimal Price { get; private set; }

    internal Movie(Guid id, string title, string director, decimal price)
    {
        Id = Guard.Against.Default(id);
        Title = Guard.Against.NullOrEmpty(title);
        Director = Guard.Against.NullOrEmpty(director);
        Price = Guard.Against.Negative(price);
    }

    internal void UpdatePrice(decimal newPrice)
    {
        Price = Guard.Against.Negative(newPrice);
    }
}