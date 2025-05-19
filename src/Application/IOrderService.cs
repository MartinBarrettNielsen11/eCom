using Domain.Entities;

namespace Service;

public interface IOrderService
{
    Task<Order> CreateOrder(Order order, CancellationToken cancellationToken);
    Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken);
    Task<bool> OrderExistsAsync(int id, CancellationToken cancellationToken);
}
