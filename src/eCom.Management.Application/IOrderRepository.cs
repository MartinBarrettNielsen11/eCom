namespace Management.Application;

public interface IOrderRepository
{
    Task<Domain.Orders.Order> CreateOrder(Domain.Orders.Order order, CancellationToken cancellationToken);
    Task<Domain.Orders.Order?> GetOrderAsync(Guid id);
    Task<bool> OrderExistsAsync(Guid id);
}
