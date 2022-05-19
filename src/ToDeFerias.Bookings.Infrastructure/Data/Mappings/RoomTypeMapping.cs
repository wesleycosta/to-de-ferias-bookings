using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

namespace ToDeFerias.Bookings.Infrastructure.Data.Mappings;

public sealed class RoomTypeMapping : IEntityTypeConfiguration<RoomType>
{
    public void Configure(EntityTypeBuilder<RoomType> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasColumnName("Name");

        builder.Property(p => p.Created)
               .HasColumnName("Created");

        builder.Property(p => p.LastUpdated)
               .HasColumnName("LastUpdated");

        builder.ToTable("RoomTypes");
    }
}
