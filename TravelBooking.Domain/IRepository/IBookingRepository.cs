using TravelBooking.Domain.Entities;
using TravelBooking.Domain.IRepository;

namespace TravelBooking.Domain.Interfaces;

public interface IBookingRepository : IGenericRepository<Booking>
{
    Task<IEnumerable<Booking>?> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<dynamic>> GetBookingsAsync(string flightNumber, int take, int skip, CancellationToken cancellationToken = default);
}