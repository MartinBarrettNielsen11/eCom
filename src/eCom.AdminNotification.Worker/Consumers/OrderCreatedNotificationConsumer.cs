namespace AdminNotification.Worker.Consumers;

public class OrderCreatedNotificationConsumer : IConsumer<OrderCreated>
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task Consume(ConsumeContext<OrderCreated> context)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        Console.WriteLine($"Order created notification received for: {context.Message.Id}");
    }
}
