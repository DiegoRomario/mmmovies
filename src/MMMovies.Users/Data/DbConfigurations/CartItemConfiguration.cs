using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMMovies.Users.Domain;

namespace MMMovies.Users.Data.DbConfigurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.Property(item => item.Id)
               .ValueGeneratedNever();
    }
}
