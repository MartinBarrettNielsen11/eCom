using Domain.Entities;

namespace Management.Application;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task<Order> CreateOrder(Order order, CancellationToken cancellationToken = default)
    {
        return await orderRepository.CreateOrder(order);
    }

    public async Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken = default)
    {
        return await orderRepository.GetOrderAsync(id);
    }

    public async Task<bool> OrderExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await orderRepository.OrderExistsAsync(id);
    }
}