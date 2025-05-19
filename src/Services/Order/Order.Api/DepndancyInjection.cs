namespace Order.Api;

public static class DepndancyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        // Add your api services here like controllers, filters, etc.
        return services;
    }
    public static WebApplication ConfigureApi(this WebApplication app)
    {
        // Configure your api middlewares here like exception handling, routing, etc.

        return app;
    }
}
