using Domain.Entities;

namespace Management.Application;

public interface IOrderService
{
    Task<Order> CreateOrder(Order order, CancellationToken cancellationToken);
    Task<Order?> GetOrderAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> OrderExistsAsync(Guid id, CancellationToken cancellationToken);
}
