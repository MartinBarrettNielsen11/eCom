using Microsoft.Extensions.DependencyInjection;
using MassTransit;

namespace Management.Infrastructure.Messaging;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services) =>
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });
}
