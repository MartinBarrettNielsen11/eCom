namespace Management.Application;

public interface IOrderService
{
    Task<Domain.Orders.Order> CreateOrder(Domain.Orders.Order order, CancellationToken cancellationToken);
    Task<Domain.Orders.Order?> GetOrderAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> OrderExistsAsync(Guid id, CancellationToken cancellationToken);
}
