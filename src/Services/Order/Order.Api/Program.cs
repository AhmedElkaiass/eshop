using Order.Api;
using Order.Application;
using Order.Infrastrucre;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddApi();
var app = builder.Build();
// Configure the HTTP request pipeline.
app.ConfigureApi();
await app.RunAsync();
