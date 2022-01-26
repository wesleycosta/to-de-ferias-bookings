using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDeFerias.Bookings.Domain.Entities.HouseGuestAggregate;

namespace ToDeFerias.Bookings.Infrastructure.Mappings;

public sealed class HouseGuestMapping : IEntityTypeConfiguration<HouseGuest>
{
    public void Configure(EntityTypeBuilder<HouseGuest> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasColumnType("VARCHAR(255)");

        builder.OwnsOne(p => p.Email,
                q => q.Property(c => c.Address)
                      .IsRequired()
                      .HasColumnName("Email")
                      .HasColumnType($"VARCHAR({Email.MaxLength})"));

        builder.OwnsOne(p => p.Cpf,
                        q => q.Property(c => c.Number)
                              .HasColumnName("Cpf")
                              .HasColumnType($"VARCHAR({Cpf.Length})"));

        builder.OwnsOne(p => p.DateOfBirth,
                        q => q.Property(c => c.Birthday)
                              .HasColumnName("DateOfBirth")
                              .HasColumnType("DATE"));

        builder.ToTable("HouseGuests");
    }
}
