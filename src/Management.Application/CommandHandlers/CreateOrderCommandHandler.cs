using Contracts.Events;
using Domain.Entities;
using Management.Application.Results;
using MassTransit;
using MediatR;

namespace Management.Application.CommandHandlers;

public record CreateOrderCommand(Guid OrderId, int CustomerId, DateTime OrderDate) : IRequest<CommandResult<Guid>>;

public class CreateOrderCommandHandler(IPublishEndpoint publishEndpoint, IOrderService orderService) 
    : IRequestHandler<CreateOrderCommand, CommandResult<Guid>>
{
    public async Task<CommandResult<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            OrderId = command.OrderId,
            CustomerId = command.CustomerId,
            OrderDate = command.OrderDate,
            OrderItems = new List<OrderItem>()
        };
        
        var createdOrder = await orderService.CreateOrder(order, cancellationToken);

        await publishEndpoint.Publish(new OrderCreated()
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