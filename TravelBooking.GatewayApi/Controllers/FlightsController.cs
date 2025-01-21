using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Common.Commands;
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
    public async Task<ActionResult<Flight>> Get(int id)
    {
        var query = new GetFlightByIdQuery { Id = id };
        var flight = await _mediator.Send(query);
        return Ok(flight);
    }

    [HttpPost]
    public async Task<ActionResult<Flight>> Create([FromBody] CreateFlightCommand command)
    {
        var flight = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = flight.Id }, flight);
    }
}

