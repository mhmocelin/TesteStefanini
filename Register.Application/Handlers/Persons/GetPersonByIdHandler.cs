using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs;
using Register.Application.Interfaces;
using Register.Application.Queries.Persons;

namespace Register.Application.Handlers.Persons;

public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, PersonResponse>
{
    private readonly IPersonService _service;

    public GetPersonByIdHandler(IPersonService service)
    {
        _service = service;
    }

    public async Task<PersonResponse?> Handle(GetPersonByIdQuery query)
    {
        return await _service.GetByIdAsync(query.Id);
    }
}