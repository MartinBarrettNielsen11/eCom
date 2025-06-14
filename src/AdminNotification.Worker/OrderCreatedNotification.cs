using System;
using System.Threading.Tasks;
using Contracts.Events;
using MassTransit;

namespace AdminNotification.Worker;

public class OrderCreatedNotification : IConsumer<OrderCreated>
{
    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        Console.WriteLine($"Order created notification received for: {context.Message.OrderId}");
    }
}
