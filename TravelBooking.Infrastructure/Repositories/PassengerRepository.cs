using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infrastructure.mssql.Persistence;

namespace TravelBooking.Infrastructure.mssql.Repositories;

public class PassengerRepository : IPassengerRepository
{
    private readonly TravelBookingDbContext _context;

    public PassengerRepository(TravelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<Passenger> GetByIdAsync(int id)
    {
        return await _context.Passengers.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id) ?? throw new InvalidOperationException("Passenger not found");
    }

    public async Task<IEnumerable<Passenger>> GetAllAsync()
    {
        return await _context.Passengers.ToListAsync();
    }

    public async Task AddAsync(Passenger Passenger)
    {
        _context.Passengers.Add(Passenger);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Passenger Passenger)
    {
        _context.Passengers.Update(Passenger);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var Passenger = await _context.Passengers.FindAsync(id);
        if (Passenger != null)
        {
            _context.Passengers.Remove(Passenger);
            await _context.SaveChangesAsync();
        }
    }
}
