using TravelBooking.Domain.Entities;
using TravelBooking.Domain.IRepository;

namespace TravelBooking.Domain.Interfaces;

public interface IPassengerRepository : IGenericRepository<Passenger>
{
    Task<IEnumerable<Passenger>?> GetAllAsync(CancellationToken cancellationToken = default);

}
