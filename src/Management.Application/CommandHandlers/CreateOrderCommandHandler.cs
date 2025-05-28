using Contracts.Events;
using Contracts.Models;
using Domain.Entities;
using Management.Application.Providers.Time;
using Management.Application.Results;
using MassTransit;
using Mediator;

namespace Management.Application.CommandHandlers;

public record CreateOrderCommand(int CustomerId, ICollection<OrderItemModel> OrderItems) : IRequest<CommandResult<Guid>>;

public class CreateOrderCommandHandler(
    IPublishEndpoint publishEndpoint, 
    IOrderService orderService,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, CommandResult<Guid>>
{
    public async ValueTask<CommandResult<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        // TO-DO: Use riok/mapperly for source generated mapping
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            CustomerId = command.CustomerId,
            OrderDate = dateTimeProvider.UtcNow,
            OrderItems = command.OrderItems.Select(item => new OrderItem
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
            OrderId = createdOrder.OrderId,
            TotalAmount = createdOrder.OrderItems.Sum(p => p.Price * p.Quantity)
        }, context =>
        {
            context.Headers.Set("header-v1", "header-v1-value");
            context.TimeToLive = TimeSpan.FromSeconds(30);
        }, cancellationToken);

        return CommandResult.Success(createdOrder.OrderId);
    }
}
