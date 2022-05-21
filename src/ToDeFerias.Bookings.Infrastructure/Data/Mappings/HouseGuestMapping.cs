using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDeFerias.Bookings.Domain.Aggregates.HouseGuestAggregate;

namespace ToDeFerias.Bookings.Infrastructure.Data.Mappings;

public sealed class HouseGuestMapping : IEntityTypeConfiguration<HouseGuest>
{
    public void Configure(EntityTypeBuilder<HouseGuest> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasColumnName("Id");

        builder.Property(p => p.Name)
               .IsRequired()
               .HasColumnName("Name");

        builder.OwnsOne(p => p.Email,
                        p => p.Property(c => c.Address)
                              .IsRequired()
                              .HasColumnName("Email"));

        builder.OwnsOne(p => p.Cpf,
                        p => p.Property(c => c.Number)
                              .IsRequired()
                              .HasColumnName("Cpf"));

        builder.OwnsOne(p => p.DateOfBirth,
                        p => p.Property(c => c.Birthday)
                              .HasColumnName("DateOfBirth"));

        builder.Property(p => p.Created)
               .HasColumnName("Created");

        builder.Property(p => p.LastUpdated)
               .HasColumnName("LastUpdated");

        builder.ToTable("HouseGuests");
    }
}
