using FluentValidation;
using Microsoft.Extensions.Options;
using Register.Application.Commands.Auth;
using Register.Application.Commands.Persons;
using Register.Application.Commands.Persons.V2;
using Register.Application.Dispatcher;
using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs;
using Register.Application.DTOs.V2;
using Register.Application.Handlers;
using Register.Application.Handlers.Persons;
using Register.Application.Handlers.Persons.V2;
using Register.Application.Interfaces;
using Register.Application.Queries.Persons;
using Register.Application.Queries.Persons.V2;
using Register.Application.Services;
using Register.Application.Validators.Persons;
using Register.Application.Validators.Persons.V2;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Register.Api.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        // Services
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IApplicationDispatcher, ApplicationDispatcher>();
        services.AddScoped<IAuthService, AuthService>();

        // Validators V1
        services.AddScoped<IValidator<PersonCreate>, CreatePersonValidator>();
        services.AddScoped<IValidator<PersonUpdate>, UpdatePersonValidator>();

        // Validators V2
        services.AddScoped<IValidator<PersonV2Create>, CreatePersonV2Validator>();
        services.AddScoped<IValidator<PersonV2Update>, UpdatePersonV2Validator>();

        // Handlers V1
        services.AddScoped<IRequestHandler<CreatePersonCommand, PersonResponse>, CreatePersonHandler>();
        services.AddScoped<IRequestHandler<UpdatePersonCommand, PersonResponse?>, UpdatePersonHandler>();
        services.AddScoped<IRequestHandler<DeletePersonCommand, bool>, DeletePersonHandler>();
        services.AddScoped<IRequestHandler<GetPersonByIdQuery, PersonResponse?>, GetPersonByIdHandler>();
        services.AddScoped<IRequestHandler<GetAllPersonsQuery, IEnumerable<PersonResponse>>, GetAllPersonsHandler>();
        services.AddScoped<IRequestHandler<LoginCommand, LoginResponse?>, LoginHandler>();

        // Handlers V2
        services.AddScoped<IRequestHandler<CreatePersonV2Command, PersonV2Response>, CreatePersonV2Handler>();
        services.AddScoped<IRequestHandler<UpdatePersonV2Command, PersonV2Response?>, UpdatePersonV2Handler>();
        services.AddScoped<IRequestHandler<DeletePersonV2Command, bool>, DeletePersonV2Handler>();
        services.AddScoped<IRequestHandler<GetPersonByIdV2Query, PersonV2Response?>, GetPersonByIdV2Handler>();
        services.AddScoped<IRequestHandler<GetAllPersonsV2Query, IEnumerable<PersonV2Response>>, GetAllPersonsV2Handler>();

        return services;
    }
}
