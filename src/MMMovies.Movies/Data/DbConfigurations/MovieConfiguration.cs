using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MMMovies.Movies.Domain;

namespace MMMovies.Movies.Data.DbConfigurations;

internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    internal static readonly Guid Movie1Guid = new("A89F6CD7-4693-457B-9009-02205DBBFE45");
    internal static readonly Guid Movie2Guid = new("E4FA19BF-6981-4E50-A542-7C9B26E9EC31");
    internal static readonly Guid Movie3Guid = new("17C61E41-3953-42CD-8F88-D3F698869B35");

    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.Property(p => p.Title)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(p => p.Director)
               .HasMaxLength(100)
               .IsRequired();

        builder.HasData(GetSampleMovieData());
    }

    private static IEnumerable<Movie> GetSampleMovieData()
    {
        var director = "Quentin Tarantino";
        yield return new Movie(Movie1Guid, "Pulp Fiction", director, 10.99m);
        yield return new Movie(Movie2Guid, "Reservoir Dogs", director, 11.99m);
        yield return new Movie(Movie3Guid, "Kill Bill", director, 12.99m);
    }
}


