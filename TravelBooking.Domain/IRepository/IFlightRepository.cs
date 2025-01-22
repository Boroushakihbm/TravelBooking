using TravelBooking.Domain.Entities;

namespace TravelBooking.Domain.Interfaces;

public interface IFlightRepository
{
    Task<Flight?> GetByIdAsync(int id);
    Task<Flight?> GetByFlightNumberAsync(string flightNumber);
    Task<IEnumerable<Flight>> GetAllAsync();
    Task AddAsync(Flight flight);
    Task UpdateAsync(Flight flight);
    Task DeleteAsync(int id);
}