using Register.Application.Commands.Persons.V2;
using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs.V2;
using Register.Application.Interfaces;

namespace Register.Application.Handlers.Persons.V2;

public class UpdatePersonV2Handler : IRequestHandler<UpdatePersonV2Command, PersonV2Response?>
{
    private readonly IPersonService _personService;

    public UpdatePersonV2Handler(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<PersonV2Response?> Handle(UpdatePersonV2Command command)
    {
        return await _personService.UpdateV2Async(command.Id, command.Dto);
    }
}
