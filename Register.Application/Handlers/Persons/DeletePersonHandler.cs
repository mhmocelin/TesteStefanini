using Register.Application.Commands.Persons;
using Register.Application.Dispatcher;
using Register.Application.Interfaces;

namespace Register.Application.Handlers.Persons;

public class DeletePersonHandler : ICommandHandler<DeletePersonCommand, bool>
{
    private readonly IPersonService _service;

    public DeletePersonHandler(IPersonService service)
    {
        _service = service;
    }

    public async Task<bool> HandleAsync(DeletePersonCommand command)
    {
        return await _service.DeleteAsync(command.Id);
    }
}