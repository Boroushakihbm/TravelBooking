﻿using TravelBooking.Domain.IRepository;

namespace TravelBooking.Domain.Entities;
public class Flight: IRootEntity
{
    public int Id { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int AvailableSeats { get; set; }
    public decimal Price { get; set; }
    public virtual ICollection<Booking>? Bookings { get; set; }
}
