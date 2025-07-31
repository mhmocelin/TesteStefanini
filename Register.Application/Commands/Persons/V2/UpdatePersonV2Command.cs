using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs.V2;

namespace Register.Application.Commands.Persons.V2;

public class UpdatePersonV2Command : IRequest<PersonV2Response?>
{
    public Guid Id { get; }
    public PersonV2Update Dto { get; }

    public UpdatePersonV2Command(Guid id, PersonV2Update dto)
    {
        Id = id;
        Dto = dto;
    }
}
