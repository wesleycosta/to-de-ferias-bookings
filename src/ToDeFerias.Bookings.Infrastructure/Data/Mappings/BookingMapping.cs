using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

namespace ToDeFerias.Bookings.Infrastructure.Data.Mappings;

public sealed class BookingMapping : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasColumnName("Id");

        builder.OwnsOne(p => p.DateRange,
                        dateRange =>
                        {
                            dateRange.Property(c => c.CheckIn)
                                     .IsRequired()
                                     .HasColumnName("CheckIn");

                            dateRange.Property(c => c.CheckOut)
                                     .IsRequired()
                                     .HasColumnName("CheckOut");
                        });

        builder.Property(p => p.Value)
               .IsRequired()
               .HasPrecision(12, 2)
               .HasColumnName("Value");

        builder.Property(p => p.Adults)
               .IsRequired()
               .HasColumnName("Adults");

        builder.Property(p => p.Children)
               .IsRequired()
               .HasColumnName("Children");

        builder.Property(p => p.Status)
               .IsRequired()
               .HasColumnName("Status")
               .HasConversion(new EnumToStringConverter<BookingStatus>())
               .HasMaxLength(255);

        builder.HasOne(p => p.HouseGuest)
               .WithMany(p => p.Bookings)
               .HasForeignKey(c => c.HouseGuestId);

        builder.HasOne(p => p.Room)
               .WithMany(p => p.Bookings)
               .HasForeignKey(p => p.RoomId);

        builder.ToTable("Bookings");
    }
}
