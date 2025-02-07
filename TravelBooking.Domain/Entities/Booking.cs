﻿using TravelBooking.Domain.Events;
using TravelBooking.Domain.IRepository;

namespace TravelBooking.Domain.Entities;

public class Booking: IRootEntity
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public DateTime BookingDate { get; set; }
    public int SeatCount { get; set; }

    public Booking() { }

    public Booking(int id, int flightId, int passengerId, DateTime bookingDate, int seatCount)
    {
        Id = id;
        FlightId = flightId;
        PassengerId = passengerId;
        BookingDate = bookingDate;
        SeatCount = seatCount;
    }
    public static Tuple<Booking, BookingCreatedEvent> Create(int flightId, int passengerId, DateTime bookingDate, int seatCount)
    {
        var id = 0;
        var booking = new Booking(id, flightId, passengerId, bookingDate, seatCount);
        var @event = new BookingCreatedEvent(id, flightId, passengerId, bookingDate, seatCount);

        return new Tuple<Booking, BookingCreatedEvent>(booking, @event);
    }
}
