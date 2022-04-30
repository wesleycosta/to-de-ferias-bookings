using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDeFerias.Bookings.Domain.Entities.BookingAggregate;

namespace ToDeFerias.Bookings.Infrastructure.Mappings;

public sealed class RoomTypeMapping : IEntityTypeConfiguration<RoomType>
{
    public void Configure(EntityTypeBuilder<RoomType> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasColumnType("VARCHAR(255)");

        builder.Property(p => p.Created)
               .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.LastUpdated)
               .HasDefaultValueSql("GETDATE()");

        builder.ToTable("RoomTypes");
    }
}
