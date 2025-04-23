using BuildingBlocks.Exceptions.Handlers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
Assembly assembly = typeof(Program).Assembly;
// Add Services To Di.... 
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LogingBehavior<,>));
});
builder.Services.AddCarter(configurator: (cfg) =>
{
});
if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitalizeData>();
}
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("DefaultConnection")!);
}).UseLightweightSessions();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
//builder.Services.AddProblemDetails();
builder.Services.AddValidatorsFromAssembly(assembly);
var app = builder.Build();
// Add Middlewares (Configure Http Request Live cycle.) , order is important here .....
app.UseExceptionHandler(options =>
{

});
app.MapCarter();
app.Run();
