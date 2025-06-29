using System;
using System.Threading.Tasks;
using Contracts.Events;
using MassTransit;

namespace AdminNotification.Worker.Consumers;

public class OrderCreatedNotificationConsumer : IConsumer<OrderCreated>
{
    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        Console.WriteLine($"Order created notification received for: {context.Message.Id}");
    }
}
