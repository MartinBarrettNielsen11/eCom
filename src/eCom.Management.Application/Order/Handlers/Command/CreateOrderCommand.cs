namespace Management.Application.Order.Handlers.Command;

public record CreateOrderCommand(int CustomerId, ICollection<OrderItemModel> OrderItems) : 
    IRequest<CommandResult<Guid>>;
