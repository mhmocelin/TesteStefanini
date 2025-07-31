using Register.Application.Commands.Persons;
using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs;
using Register.Application.Interfaces;

namespace Register.Application.Handlers.Persons;

public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, PersonResponse>
{
    private readonly IPersonService _service;

    public CreatePersonHandler(IPersonService service)
    {
        _service = service;
    }

    public async Task<PersonResponse> Handle(CreatePersonCommand command)
    {
        return await _service.CreateAsync(command.Person);
    }
}