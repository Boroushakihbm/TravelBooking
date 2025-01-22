namespace TravelBooking.Common.Commands.Passenger;

public class CreatePassengerCommand : IRequest<Domain.Entities.Passenger>
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
}
