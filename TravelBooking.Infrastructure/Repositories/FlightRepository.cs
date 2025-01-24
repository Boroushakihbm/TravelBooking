using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infrastructure.mssql.Persistence;
using Microsoft.Extensions.Caching.Memory;

namespace TravelBooking.Infrastructure.mssql.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly TravelBookingDbContext _context;
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheOptions;

    public FlightRepository(TravelBookingDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
        _cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };
    }

    public async Task<Flight?> GetByIdAsync(int id)
    {
        if (!_cache.TryGetValue($"Flight_{id}", out Flight? flight))
        {
            flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _cache.Set($"Flight_{id}", flight, _cacheOptions);
            }
        }
        return flight ?? throw new KeyNotFoundException("Flight not found");
    }

    public async Task<IEnumerable<Flight>> GetAllAsync()
    {
        if (!_cache.TryGetValue("AllFlights", out IEnumerable<Flight>? flights))
        {
            flights = await _context.Flights.ToListAsync();
            _cache.Set("AllFlights", flights, _cacheOptions);
        }
        return flights ?? Enumerable.Empty<Flight>();
    }

    public async Task AddAsync(Flight flight)
    {
        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();
        _cache.Remove("AllFlights");
    }

    public async Task UpdateAsync(Flight flight)
    {
        _context.Flights.Update(flight);
        await _context.SaveChangesAsync();
        _cache.Remove($"Flight_{flight.Id}");
        _cache.Remove("AllFlights");
    }

    public async Task DeleteAsync(int id)
    {
        var flight = await _context.Flights.FindAsync(id);
        if (flight != null)
        {
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            _cache.Remove($"Flight_{id}");
            _cache.Remove("AllFlights");
        }
    }

    public async Task<Flight?> GetByFlightNumberAsync(string flightNumber)
    {
        if (!_cache.TryGetValue($"FlightNumber_{flightNumber}", out Flight? flight))
        {
            flight = await _context.Flights.Where(f => f.FlightNumber == flightNumber).FirstOrDefaultAsync();
            if (flight != null)
            {
                _cache.Set($"FlightNumber_{flightNumber}", flight, _cacheOptions);
            }
        }
        return flight;
    }

    public async Task<List<Flight>?> GetByFilterAsync(Func<Flight, bool> filterFlight, int take, int skip)
    {
        if (!_cache.TryGetValue("AllFlights", out IEnumerable<Flight>? flights))
        {
            flights = await _context.Flights.AsNoTracking().ToListAsync();
            _cache.Set("AllFlights", flights, _cacheOptions);
        }

        return flights?.Where(filterFlight).Take(take).Skip(skip).ToList() ?? new List<Flight>();
    }
}