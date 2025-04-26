using BuildingBlocks.Exceptions.Handlers;
using HealthChecks.UI.Client;
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
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);
var app = builder.Build();
// Add Middlewares (Configure Http Request Live cycle.) , order is important here .....
app.UseExceptionHandler(options =>
{

});
app.MapCarter();
app.UseHealthChecks("/health", options: new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
await app.RunAsync();