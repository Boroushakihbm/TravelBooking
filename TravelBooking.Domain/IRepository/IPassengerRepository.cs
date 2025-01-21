using TravelBooking.Domain.Entities;

namespace TravelBooking.Domain.Interfaces;

public interface IPassengerRepository { 
    Task<Passenger> GetByIdAsync(int id); 
    Task<IEnumerable<Passenger>> GetAllAsync(); 
    Task AddAsync(Passenger passenger); 
    Task UpdateAsync(Passenger passenger); 
    Task DeleteAsync(int id); 
}
