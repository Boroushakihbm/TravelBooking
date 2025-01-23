using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infrastructure.mssql.Persistence;

namespace TravelBooking.Infrastructure.mssql.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly TravelBookingDbContext _context;

    public BookingRepository(TravelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<Booking> GetByIdAsync(int id)
    {
        return await _context.Bookings.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id) ?? throw new InvalidOperationException("Booking not found");
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await _context.Bookings.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(Booking Booking)
    {
        await _context.Bookings.AddAsync(Booking);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Booking Booking)
    {
        _context.Bookings.Update(Booking);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var Booking = await _context.Bookings.FindAsync(id);
        if (Booking != null)
        {
            _context.Bookings.Remove(Booking);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<dynamic>> GetBookingsAsync(string flightNumber, int take, int skip)
    {
        var result = await (from booking in _context.Bookings
                            join flight in _context.Flights on booking.FlightId equals flight.Id
                            join passenger in _context.Passengers on booking.PassengerId equals passenger.Id
                            where flight.FlightNumber == flightNumber
                            select new 
                            {
                                Id = booking.Id,
                                SeatCount = booking.SeatCount,
                                Origin = flight.Origin,
                                Destination = flight.Destination,
                                DepartureTime = flight.DepartureTime,
                                FullName = passenger.FullName,
                                BookingDate = booking.BookingDate
                            }).ToListAsync();

        return result;
    }
}