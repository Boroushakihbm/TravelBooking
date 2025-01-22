using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Entities;

namespace TravelBooking.Infrastructure.mssql.SeedsData;

public static class ModelBuilderExtensions
{
    public static void SeedsData(this ModelBuilder modelBuilder)
    {
        // Seed data for Flights
        modelBuilder.Entity<Flight>().HasData(
            new Flight { Id = 1, FlightNumber = "AB123", Origin = "Tehran", Destination = "Mashhad", DepartureTime = DateTime.Now.AddHours(2), ArrivalTime = DateTime.Now.AddHours(4), AvailableSeats = 100, Price = 200.00m },
            new Flight { Id = 2, FlightNumber = "CD456", Origin = "Tehran", Destination = "Kish", DepartureTime = DateTime.Now.AddHours(5), ArrivalTime = DateTime.Now.AddHours(7), AvailableSeats = 150, Price = 300.00m }
        );

        // Seed data for Passengers
        modelBuilder.Entity<Passenger>().HasData(
            new Passenger { Id = 1, FullName = "Pooyan Boroushaki", Email = "test1@gmail.com", PassportNumber = "A1234567", PhoneNumber = "09356092381" },
            new Passenger { Id = 2, FullName = "Saman Ahmadi", Email = "test2@gmail.com", PassportNumber = "B7654321", PhoneNumber = "09356092382" }
        );

        // Seed data for Bookings
        modelBuilder.Entity<Booking>().HasData(
            new Booking { Id = 1, FlightId = 1, PassengerId = 1, BookingDate = DateTime.Now, SeatCount = 1 },
            new Booking { Id = 2, FlightId = 2, PassengerId = 2, BookingDate = DateTime.Now, SeatCount = 1 }
        );
    }
}
