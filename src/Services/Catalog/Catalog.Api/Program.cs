

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
//app.Use((context, task) =>
//{
//    Stopwatch stopwatch = new Stopwatch();
//    stopwatch.Start();
//    var next = task();
//    stopwatch.Stop();
//    var elapsed = stopwatch.ElapsedMilliseconds;
//    builder.Services.BuildServiceProvider()?.GetRequiredService<ILogger<Program>>()
//        .LogInformation("Request {method} {path} took {elapsed} ms", context.Request.Method, context.Request.Path, elapsed);
//    return next;
//});
app.MapCarter();
app.Run();
