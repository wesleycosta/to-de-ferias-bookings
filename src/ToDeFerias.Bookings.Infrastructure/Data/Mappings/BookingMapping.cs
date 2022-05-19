using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

namespace ToDeFerias.Bookings.Infrastructure.Data.Mappings;

public sealed class BookingMapping : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(p => p.Id)
               .HasName("Id");

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
               .HasConversion(new EnumToStringConverter<BookingStatus>());

        builder.Property(p => p.Created)
               .HasColumnName("Created");

        builder.Property(p => p.LastUpdated)
               .HasColumnName("Created");

        builder.HasOne(c => c.HouseGuest)
               .WithMany(c => c.Bookings)
               .HasForeignKey(c => c.HouseGuestId);

        builder.HasOne(c => c.Room)
               .WithMany(c => c.Bookings)
               .HasForeignKey(c => c.RoomId);

        builder.ToTable("Bookings");
    }
}
