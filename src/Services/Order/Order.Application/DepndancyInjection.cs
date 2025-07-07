using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Application;

public static class DepndancyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Add your application services here like mediator , validators, etc.
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
