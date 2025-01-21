using TravelBooking.Domain.Entities;

namespace TravelBooking.Domain.Interfaces;

public interface IBookingRepository { 
    Task<Booking> GetByIdAsync(int id); 
    Task<IEnumerable<Booking>> GetAllAsync(); 
    Task AddAsync(Booking booking); 
    Task UpdateAsync(Booking booking); 
    Task DeleteAsync(int id); 
}