using TravelBooking.Domain.Entities;
namespace TravelBooking.Domain.Interfaces;
using System.Linq.Expressions;
public interface IFlightRepository
{
    Task<Flight?> GetByIdAsync(int id);
    Task<List<Flight>?> GetByFilterAsync(Func<Flight, bool> filterFlight, int take, int skip);
    Task<Flight?> GetByFlightNumberAsync(string flightNumber);
    Task<IEnumerable<Flight>> GetAllAsync();
    Task AddAsync(Flight flight);
    Task UpdateAsync(Flight flight);
    Task DeleteAsync(int id);
}