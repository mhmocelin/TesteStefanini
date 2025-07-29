using Register.Application.Commands.Persons;
using Register.Application.Dispatcher;
using Register.Application.DTOs;
using Register.Application.Interfaces;

namespace Register.Application.Handlers.Persons;

public class UpdatePersonHandler : ICommandHandler<UpdatePersonCommand, PersonResponse>
{
    private readonly IPersonService _service;

    public UpdatePersonHandler(IPersonService service)
    {
        _service = service;
    }

    public async Task<PersonResponse> HandleAsync(UpdatePersonCommand command)
    {
        return await _service.UpdateAsync(command.Id, command.Request);
    }
}