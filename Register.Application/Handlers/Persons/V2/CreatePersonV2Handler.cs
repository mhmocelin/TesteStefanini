using Register.Application.Commands.Persons.V2;
using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs.V2;
using Register.Application.Interfaces;

namespace Register.Application.Handlers.Persons.V2
{
    public class CreatePersonV2Handler : IRequestHandler<CreatePersonV2Command, PersonV2Response>
    {
        private readonly IPersonService _personService;

        public CreatePersonV2Handler(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<PersonV2Response> Handle(CreatePersonV2Command request)
        {
            return await _personService.CreateV2Async(request.Person);
        }
    }
}
