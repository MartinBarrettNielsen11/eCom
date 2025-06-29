using Domain.Entities;

namespace Management.Application;

public interface IOrderRepository
{
    Task<Order> CreateOrder(Order order, CancellationToken cancellationToken);
    Task<Order?> GetOrderAsync(Guid id);
    Task<bool> OrderExistsAsync(Guid id);
}