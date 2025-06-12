using Contracts.Events;
using MassTransit;

namespace Management.Infrastructure.Consumer;

public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        await Task.Delay(1000);
        Console.WriteLine($"Consumed message with OrderId: {context.Message.OrderId} " +
                          $"which was created at: {context.Message.CreatedAt}" );
    }
}