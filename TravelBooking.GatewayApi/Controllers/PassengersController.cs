using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Common.Commands.Passenger;
using TravelBooking.Common.Queries.Passenger;
using TravelBooking.Domain.Entities;

namespace TravelBooking.GatewayApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PassengersController : ControllerBase
{
    private readonly IMediator _mediator;

    public PassengersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Passenger>> Get(int id)
    {
        var query = new GetPassengerByIdQuery { Id = id };
        var Passenger = await _mediator.Send(query);
        return Ok(Passenger);
    }
    
    [HttpPost]
    public async Task<ActionResult<Passenger>> Create([FromBody] CreatePassengerCommand command)
    {
        var Passenger = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = Passenger.Id }, Passenger);
    }
}

