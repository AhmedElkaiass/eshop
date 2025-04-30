using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handlers;
using Carter;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Assembly assembly = typeof(Program).Assembly;
// Add services to the container.
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LogingBehavior<,>));
});
builder.Services.AddCarter();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
//builder.Services.AddProblemDetails();
builder.Services.AddValidatorsFromAssembly(assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.MapCarter();
app.Run();
