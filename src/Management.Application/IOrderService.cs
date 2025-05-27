using Domain.Entities;

namespace Management.Application;

public interface IOrderService
{
    
    Task<Order> CreateOrder(Order order, CancellationToken cancellationToken);
    Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken);
    Task<bool> OrderExistsAsync(int id, CancellationToken cancellationToken);
}
