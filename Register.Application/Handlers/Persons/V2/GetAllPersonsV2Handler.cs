using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs.V2;
using Register.Application.Interfaces;
using Register.Application.Queries.Persons.V2;

namespace Register.Application.Handlers.Persons.V2;

public class GetAllPersonsV2Handler : IRequestHandler<GetAllPersonsV2Query, IEnumerable<PersonV2Response>>
{
    private readonly IPersonService _personService;

    public GetAllPersonsV2Handler(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<IEnumerable<PersonV2Response>> Handle(GetAllPersonsV2Query query)
    {
        return await _personService.GetAllV2Async();
    }
}
