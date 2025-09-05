namespace Management.Application.Order.Handlers.Command;

public sealed class CreateOrderCommandHandler(
    IPublishEndpoint publishEndpoint, 
    IOrderService orderService,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, Result<Guid>>
{
    public async ValueTask<Result<Guid>> Handle(
        CreateOrderCommand command, 
        CancellationToken cancellationToken)
    {
        var order = new Domain.Orders.Order()
        {
            CustomerId = command.CustomerId,
            OrderDate = dateTimeProvider.UtcNow,
            OrderItems = Enumerable.Select<OrderItemModel, OrderItem>(command.OrderItems, item => new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList()
        };
        
        var createdOrder = await orderService.CreateOrder(order, cancellationToken);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await publishEndpoint.Publish(new OrderCreated
        {
            CreatedAt = createdOrder.OrderDate, 
            Id = createdOrder.Id,
            TotalAmount = createdOrder.OrderItems.Sum(p => p.Price * p.Quantity)
        }, context =>
        {
            context.Headers.Set("header-v1", "header-v1-value");
            context.TimeToLive = TimeSpan.FromSeconds(30);
        }, cancellationToken);
        
        return createdOrder.Id;
    }
}
