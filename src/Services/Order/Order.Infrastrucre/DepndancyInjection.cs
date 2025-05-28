using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastrucre.Data.Interceptors;
namespace Order.Infrastrucre;
public static class DepndancyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add your infrastructure services here like database context, repositories, etc.
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.AddInterceptors(new AuditableInterceptor());
        });
        //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        return services;
    }
}
