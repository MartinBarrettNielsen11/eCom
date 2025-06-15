using System;
using MassTransit;

namespace AdminNotification.Worker.Consumers;

public class OrderCreatedNotificationConsumerDefinition : ConsumerDefinition<OrderCreatedNotificationConsumer>
{
    public OrderCreatedNotificationConsumerDefinition()
    {
        EndpointName = "order-created-notification";
        
        Endpoint(e =>
        {
            e.Name = "order-created-notification";
            e.ConcurrentMessageLimit = 10;
        });
    }
    
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<OrderCreatedNotificationConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.PrefetchCount = 10;
        consumerConfigurator.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
    }
}