using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMMovies.Users.Domain;

namespace MMMovies.Users.Data.DbConfigurations;

public class UserStreetAddressConfiguration : IEntityTypeConfiguration<UserStreetAddress>
{
    public void Configure(EntityTypeBuilder<UserStreetAddress> builder)
    {
        builder.ToTable(nameof(UserStreetAddress));
        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.ComplexProperty(usa => usa.StreetAddress);
    }
}

