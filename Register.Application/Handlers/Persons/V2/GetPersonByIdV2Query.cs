using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs.V2;
using Register.Application.Interfaces;
using Register.Application.Queries.Persons.V2;

namespace Register.Application.Handlers.Persons.V2;

public class GetPersonByIdV2Handler : IRequestHandler<GetPersonByIdV2Query, PersonV2Response?>
{
    private readonly IPersonService _personService;

    public GetPersonByIdV2Handler(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<PersonV2Response?> Handle(GetPersonByIdV2Query query)
    {
        return await _personService.GetByIdV2Async(query.Id);
    }
}