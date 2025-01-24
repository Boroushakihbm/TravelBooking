using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infrastructure.mssql.Persistence;

namespace TravelBooking.Infrastructure.mssql.Repositories;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{

    public BookingRepository(TravelBookingDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<IEnumerable<Booking>?> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Bookings.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<dynamic>> GetBookingsAsync(string flightNumber, int take, int skip, CancellationToken cancellationToken)
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