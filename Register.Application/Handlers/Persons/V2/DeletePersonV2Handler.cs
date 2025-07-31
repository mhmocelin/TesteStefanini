using Register.Application.Commands.Persons.V2;
using Register.Application.Dispatcher.Interfaces;
using Register.Application.Interfaces;

namespace Register.Application.Handlers.Persons.V2;

public class DeletePersonV2Handler : IRequestHandler<DeletePersonV2Command, bool>
{
    private readonly IPersonService _personService;

    public DeletePersonV2Handler(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<bool> Handle(DeletePersonV2Command request)
    {
        return await _personService.DeleteAsync(request.Id);
    }
}
