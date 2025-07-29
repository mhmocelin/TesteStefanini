using Register.Application.Dispatcher;
using Register.Application.DTOs;
using Register.Application.Interfaces;
using Register.Application.Queries.Persons;

namespace Register.Application.Handlers.Persons;

public class GetAllPersonsHandler : IQueryHandler<GetAllPersonsQuery, IEnumerable<PersonResponse>>
{
    private readonly IPersonService _service;

    public GetAllPersonsHandler(IPersonService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<PersonResponse>> HandleAsync(GetAllPersonsQuery query)
    {
        return await _service.GetAllAsync();
    }
}