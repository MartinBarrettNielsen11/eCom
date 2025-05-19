using Domain.Entities;

namespace Service;

public interface IOrderService
{
    Task<Order> CreateOrder(Order order);
    Task<Order?> GetOrderAsync(int id);
    Task<bool> OrderExistsAsync(int id);
}
