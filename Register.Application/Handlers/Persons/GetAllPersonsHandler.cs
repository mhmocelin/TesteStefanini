using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs;
using Register.Application.Interfaces;
using Register.Application.Queries.Persons;

public class GetAllPersonsHandler : IRequestHandler<GetAllPersonsQuery, IEnumerable<PersonResponse>>
{
    private readonly IPersonService _service;

    public GetAllPersonsHandler(IPersonService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<PersonResponse>> Handle(GetAllPersonsQuery query)
    {
        return await _service.GetAllAsync();
    }
}