using System.Linq.Expressions;
using TravelBooking.Domain.Entities;

namespace TravelBooking.Domain.Interfaces;

public interface IBookingRepository { 
    Task<Booking> GetByIdAsync(Guid id); 
    Task<IEnumerable<Booking>> GetAllAsync();
    Task<IEnumerable<dynamic>> GetBookingsAsync(string flightNumber, int take, int skip);
    Task AddAsync(Booking booking); 
    Task UpdateAsync(Booking booking); 
    Task DeleteAsync(int id); 
}