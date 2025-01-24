using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Common.Commands.Booking;
using TravelBooking.Common.Queries.Booking;
using TravelBooking.Domain.Entities;

namespace TravelBooking.GatewayApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IBus _bus;
    public BookingsController(IMediator mediator, IBus bus)
    {
        _mediator = mediator;
        _bus = bus;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Booking>> Get(int id)
    {
        var query = new GetBookingByIdQuery { Id = id };
        var Booking = await _mediator.Send(query);
        return Ok(Booking);
    }

    [HttpGet("/api/Bookings/")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllBookingsQuery filterParam, CancellationToken cancellationToken)
    {
        var flight = await _mediator.Send(filterParam, cancellationToken);
        return Ok(flight);
    }

    [HttpPost]
    public async Task<ActionResult<Booking>> Create([FromBody] CreateBookingCommand command)
    {
        var Booking = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = Booking.Id }, Booking);
    }
}

