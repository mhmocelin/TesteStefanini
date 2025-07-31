using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Register.Api.Controllers;
using Register.Application.Commands.Persons;
using Register.Application.Dispatcher;
using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs;
using Register.Application.Handlers.Persons;
using Register.Application.Interfaces;
using Register.Application.Queries.Persons;
using Register.Application.Services;
using Register.Application.Validators.Persons;
using Register.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("PersonsDb"));

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IApplicationDispatcher, ApplicationDispatcher>();
builder.Services.AddScoped<IRequestHandler<CreatePersonCommand, PersonResponse>, CreatePersonHandler>();
builder.Services.AddScoped<IRequestHandler<DeletePersonCommand, bool>, DeletePersonHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllPersonsQuery, IEnumerable<PersonResponse>>, GetAllPersonsHandler>();
builder.Services.AddScoped<IRequestHandler<GetPersonByIdQuery, PersonResponse>, GetPersonByIdHandler>();
builder.Services.AddScoped<IRequestHandler<UpdatePersonCommand, PersonResponse>, UpdatePersonHandler>();

builder.Services.AddScoped<IValidator<PersonCreate>, CreatePersonValidator>();
builder.Services.AddScoped<IValidator<PersonUpdate>, UpdatePersonValidator>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiResponseFilter>();
});


var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapFallbackToFile("index.html");

app.MapControllers();

app.Run();
