using TravelBooking.Domain.Entities;
namespace TravelBooking.Domain.Interfaces;
using System.Linq.Expressions;
using TravelBooking.Domain.IRepository;

public interface IFlightRepository : IGenericRepository<Flight>
{
    Task<IEnumerable<Flight>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<Flight>?> GetByFilterAsync(Func<Flight, bool> filterFlight, int take, int skip, CancellationToken cancellationToken = default);
    Task<Flight?> GetByFlightNumberAsync(string flightNumber, CancellationToken cancellationToken = default);
}