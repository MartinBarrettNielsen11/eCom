using System.Reflection.PortableExecutable;
using Management.Infrastructure.Consumer;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;

namespace Management.Infrastructure.Messaging;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services) =>
        services.AddMassTransit(x =>
        {
            x.AddConsumer<OrderCreatedConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ReceiveEndpoint("order-created", e =>
                {
                    e.ConfigureConsumer<OrderCreatedConsumer>(context);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
}
