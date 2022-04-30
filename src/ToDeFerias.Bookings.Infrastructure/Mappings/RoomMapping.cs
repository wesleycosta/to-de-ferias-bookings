using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDeFerias.Bookings.Domain.Entities.BookingAggregate;

namespace ToDeFerias.Bookings.Infrastructure.Mappings;

public sealed class RoomMapping : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Number)
               .IsRequired()
               .HasColumnType("TINYINT");

        builder.HasOne(p => p.Type)
               .WithMany(p => p.Rooms)
               .HasForeignKey(p => p.RoomTypeId);

        builder.Property(p => p.Created)
               .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.LastUpdated)
               .HasDefaultValueSql("GETDATE()");

        builder.ToTable("Rooms");
    }
}
