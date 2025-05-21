using Application.Results;
using Contracts.Events;
using MassTransit;
using MediatR;

namespace Service.CommandHandlers;

public record CreateOrderCommand(Guid OrderId, int CustomerId, DateTime OrderDate) : IRequest<CommandResult<EmptyResult>>;

public class CreateOrderCommandHandler(IPublishEndpoint publishEndpoint, IOrderService orderService) 
    : IRequestHandler<CreateOrderCommand, CommandResult<EmptyResult>>
{
    public async Task<CommandResult<EmptyResult>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
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
        
        
        return _requestHandlerImplementation.Handle(request, cancellationToken);
    }
}