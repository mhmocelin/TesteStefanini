using Microsoft.EntityFrameworkCore;
using Register.Application.Dispatcher;
using Register.Application.Interfaces;
using Register.Application.Services;
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
builder.Services.Scan(scan => scan
    .FromApplicationDependencies()
    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
    .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
