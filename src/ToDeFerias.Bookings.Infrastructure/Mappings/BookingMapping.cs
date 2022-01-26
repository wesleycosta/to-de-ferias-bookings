using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDeFerias.Bookings.Domain.Entities.BookingAggregate;

namespace ToDeFerias.Bookings.Infrastructure.Mappings;

public sealed class BookingMapping : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(p => p.Id);

        builder.OwnsOne(p => p.DateRange,
                        dateRange =>
                        {
                            dateRange.Property(c => c.CheckIn)
                                     .HasColumnName("CheckIn");

                            dateRange.Property(c => c.CheckOut)
                                     .HasColumnName("CheckOut");
                        });

        builder.Property(p => p.Value)
               .IsRequired()
               .HasColumnType("DECIMAL(12, 2)");

        builder.Property(p => p.Adults)
               .IsRequired()
               .HasColumnType("TINYINT");

        builder.Property(p => p.Children)
               .IsRequired()
               .HasColumnType("TINYINT");

        builder.Property(p => p.Status)
               .IsRequired()
               .HasConversion(new EnumToStringConverter<BookingStatus>());

        builder.HasOne(c => c.HouseGuest)
               .WithMany(c => c.Bookings)
               .HasForeignKey(c => c.HouseGuestId);

        builder.HasOne(c => c.Room)
               .WithMany(c => c.Bookings)
               .HasForeignKey(c => c.RoomId);

        builder.ToTable("Bookings");
    }
}
