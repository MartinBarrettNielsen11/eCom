using Management.Application.notSure;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(ApplicationAssembly.Get());
        }).AddScoped<IDateTimeProvider, DateTimeProvider>();
    }
}
