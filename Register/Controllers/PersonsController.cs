using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Register.Application.Commands.Persons;
using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs;
using Register.Application.Queries.Persons;

namespace Register.Api.Controllers;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]

public class PersonsController : ControllerBase
{
    private readonly IApplicationDispatcher _dispatcher;

    public PersonsController(IApplicationDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PersonResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await _dispatcher.Send(new GetAllPersonsQuery()));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
        => Ok(await _dispatcher.Send(new GetPersonByIdQuery(id)));

    [HttpPost]
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] PersonCreate dto)
        => Ok(await _dispatcher.Send(new CreatePersonCommand(dto)));

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] PersonUpdate dto)
        => Ok(await _dispatcher.Send(new UpdatePersonCommand(id, dto)));

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
        => Ok(await _dispatcher.Send(new DeletePersonCommand(id)));
}
