using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelBooking.Common.Exceptions;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infrastructure.mssql.Persistence;

namespace TravelBooking.Infrastructure.mssql.Repositories;

public class PassengerRepository : GenericRepository<Passenger>, IPassengerRepository
{
    public PassengerRepository(TravelBookingDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<IEnumerable<Passenger>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Passengers.ToListAsync();
    }
}
