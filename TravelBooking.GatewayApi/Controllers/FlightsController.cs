using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Common.Commands.Flight;
using TravelBooking.Common.Queries.Flight;
using TravelBooking.Domain.Entities;

namespace TravelBooking.GatewayApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FlightsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Flight>> Get(int id, CancellationToken cancellationToken)
    {
        var query = new GetFlightByIdQuery { Id = id };
        var flight = await _mediator.Send(query, cancellationToken);
        return Ok(flight);
    }

    [HttpGet("/api/Flights/")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllFlightsByQuery filterParam, CancellationToken cancellationToken)
    {
        var flight = await _mediator.Send(filterParam, cancellationToken);
        return Ok(flight);
    }

    [HttpPost]
    public async Task<ActionResult<Flight>> Create([FromBody] CreateFlightCommand command, CancellationToken cancellationToken)
    {
        var flight = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = flight.Id }, flight);
    }

    [HttpPut]
    public async Task<ActionResult<Flight>> Update([FromBody] UpdateFlightAvailableSeatCommand command, CancellationToken cancellationToken)
    {
        var update = await _mediator.Send(command, cancellationToken);
        return AcceptedAtAction(nameof(Get), new { id = update.Id }, update);
    }
}

