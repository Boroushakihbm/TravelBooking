using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBooking.Domain.Entities;

namespace TravelBooking.Infrastructure.mssql.EntitiesConfig;

public class PassengerConfig : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.ToTable("Passenger");
        builder.HasMany(c => c.Bookings).WithOne().HasForeignKey(d => d.PassengerId);
    }
}