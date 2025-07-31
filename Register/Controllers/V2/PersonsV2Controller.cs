using Microsoft.AspNetCore.Mvc;
using Register.Application.Commands.Persons.V2;
using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs.V2;
using Register.Application.Queries.Persons.V2;

namespace Register.Api.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IApplicationDispatcher _dispatcher;

    public PersonsController(IApplicationDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PersonV2Response>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await _dispatcher.Send(new GetAllPersonsV2Query()));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PersonV2Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
        => Ok(await _dispatcher.Send(new GetPersonByIdV2Query(id)));

    [HttpPost]
    [ProducesResponseType(typeof(PersonV2Response), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] PersonV2Create dto)
        => Ok(await _dispatcher.Send(new CreatePersonV2Command(dto)));

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PersonV2Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] PersonV2Update dto)
        => Ok(await _dispatcher.Send(new UpdatePersonV2Command(id, dto)));

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
        => Ok(await _dispatcher.Send(new DeletePersonV2Command(id)));
}
