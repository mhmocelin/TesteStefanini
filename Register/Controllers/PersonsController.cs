using Microsoft.AspNetCore.Mvc;
using Register.Application.Commands.Persons;
using Register.Application.Dispatcher;
using Register.Application.DTOs;
using Register.Application.Queries.Persons;

namespace Register.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IApplicationDispatcher _dispatcher;

    public PersonsController(IApplicationDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonResponse>>> GetAll()
    {
        var query = new GetAllPersonsQuery();
        var persons = await _dispatcher.SendQueryAsync(query);
        return Ok(persons);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PersonResponse>> GetById(Guid id)
    {
        var query = new GetPersonByIdQuery(id);
        var person = await _dispatcher.SendQueryAsync(query);
        if (person == null)
            return NotFound();

        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<PersonResponse>> Create([FromBody] PersonCreate dto)
    {
        var command = new CreatePersonCommand(dto);
        var person = await _dispatcher.SendCommandAsync(command);
        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PersonResponse>> Update(Guid id, [FromBody] PersonUpdate dto)
    {
        var command = new UpdatePersonCommand(id, dto);
        var person = await _dispatcher.SendCommandAsync(command);
        if (person == null)
            return NotFound();

        return Ok(person);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeletePersonCommand(id);
        var deleted = await _dispatcher.SendCommandAsync(command);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
