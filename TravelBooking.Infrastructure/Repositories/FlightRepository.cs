using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infrastructure.mssql.Persistence;
using System.Linq.Expressions;
namespace TravelBooking.Infrastructure.mssql.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly TravelBookingDbContext _context;

    public FlightRepository(TravelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<Flight?> GetByIdAsync(int id) => await _context.Flights.FindAsync(id);
    
    public async Task<IEnumerable<Flight>> GetAllAsync() => await _context.Flights.ToListAsync();

    public async Task AddAsync(Flight flight)
    {
        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Flight flight)
    {
        _context.Flights.Update(flight);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var flight = await _context.Flights.FindAsync(id);
        if (flight != null)
        {
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Flight?> GetByFlightNumberAsync(string flightNumber) =>
    await _context.Flights.Where(flight => flight.FlightNumber.Equals(flightNumber)).FirstOrDefaultAsync();

    public async Task<List<Flight>?> GetByFilterAsync(Expression<Func<Flight, bool>> filterFlight,
                                                      int take,
                                                      int skip)
    {
        return await _context.Flights
                .Where(filterFlight)
                .AsNoTracking()
                .Take(take)
                .Skip(skip)
                .ToListAsync();
    }
    
}
