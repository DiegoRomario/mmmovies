using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMMovies.OrderProcessing.Domain;

namespace MMMovies.OrderProcessing.Data.DbConfigurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    void IEntityTypeConfiguration<OrderItem>.Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Description).HasMaxLength(100)
                                            .IsRequired();
    }
}


