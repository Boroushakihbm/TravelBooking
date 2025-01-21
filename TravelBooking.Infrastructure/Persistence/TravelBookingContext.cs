
using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Entities;

namespace TravelBooking.Infrastructure.mssql.Persistence;

public class TravelBookingContext : DbContext
{
    public TravelBookingContext(DbContextOptions<TravelBookingContext> options) : base(options) { }

    public DbSet<Flight> Flights { get; set; }
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
}

