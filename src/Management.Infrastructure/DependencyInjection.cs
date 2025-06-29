using Management.Application;
using Management.Application.Providers.Time;
using Management.Infrastructure.Messaging;
using Management.Infrastructure.Time;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddMessaging()
            .AddApplication()
            .AddScoped<IDateTimeProvider, DateTimeProvider>();
}