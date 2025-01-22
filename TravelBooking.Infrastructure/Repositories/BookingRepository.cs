using Microsoft.EntityFrameworkCore;
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

    public async Task<Booking?> GetByIdAsync(int id)
    {
        return await _context.Bookings.FindAsync(id);
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await _context.Bookings.ToListAsync();
    }

    public async Task AddAsync(Booking Booking)
    {
        _context.Bookings.Add(Booking);
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
}
