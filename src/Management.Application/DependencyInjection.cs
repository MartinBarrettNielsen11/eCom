using Management.Application.notSure;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddMediator(options => options.ServiceLifetime = ServiceLifetime.Transient)
            .AddScoped<IDateTimeProvider, DateTimeProvider>();
}
