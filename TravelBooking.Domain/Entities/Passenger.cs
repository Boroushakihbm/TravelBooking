using TravelBooking.Domain.IRepository;

namespace TravelBooking.Domain.Entities;

public class Passenger: IRootEntity
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public virtual ICollection<Booking>? Bookings { get; set; }
}
