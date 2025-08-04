using Contracts.Events;
using Contracts.Models;
using Domain.Entities;
using Management.Application.MappersV2;
using Management.Application.Providers.Time;
using Management.Application.Results;
using MassTransit;
using Mediator;

namespace Management.Application.CommandHandlers;

public record CreateOrderCommand(int CustomerId, ICollection<OrderItemModel> OrderItems) : IRequest<CommandResult<Guid>>;

public sealed class CreateOrderCommandHandler(
    IPublishEndpoint publishEndpoint, 
    IOrderService orderService,
    IDateTimeProvider dateTimeProvider,
    OrderMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, CommandResult<Guid>>
{
    public async ValueTask<CommandResult<Guid>> Handle(
        CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = mapper.MapToOrder(command);
        // TO-DO: Determine a way around this issue when using riok/mapperly
        // old approach is used below
        order.OrderDate = dateTimeProvider.UtcNow;

        var order2 = new Order
        {
            Id = order.Id,
            CustomerId = command.CustomerId,
            OrderDate = dateTimeProvider.UtcNow,
            OrderItems = command.OrderItems.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList()
        };
        
        
        var createdOrder = await orderService.CreateOrder(order2, cancellationToken);
        
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
        
        return CommandResult.Success(createdOrder.Id);
    }
}
