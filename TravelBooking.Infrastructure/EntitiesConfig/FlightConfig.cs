using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBooking.Domain.Entities;

namespace TravelBooking.Infrastructure.mssql.EntitiesConfig;

public class FlightConfig : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.ToTable("Flight");

        builder.HasMany(c => c.Bookings).WithOne().HasForeignKey(d => d.FlightId);
    }
}
