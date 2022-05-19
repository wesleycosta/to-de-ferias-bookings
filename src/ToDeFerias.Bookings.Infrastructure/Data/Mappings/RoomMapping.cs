using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

namespace ToDeFerias.Bookings.Infrastructure.Data.Mappings;

public sealed class RoomMapping : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Number)
               .IsRequired()
               .HasColumnName("Number");

        builder.Property(p => p.Created)
          .HasColumnName("Created");

        builder.Property(p => p.LastUpdated)
               .HasColumnName("LastUpdated");

        builder.HasOne(p => p.Type)
               .WithMany(p => p.Rooms)
               .HasForeignKey(p => p.RoomTypeId);

        builder.ToTable("Rooms");
    }
}
