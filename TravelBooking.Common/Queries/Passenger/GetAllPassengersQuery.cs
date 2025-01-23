namespace TravelBooking.Common.Queries.Passenger;

public class GetAllPassengersQuery
{
    public string? FullName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? PassportNumber { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
}
