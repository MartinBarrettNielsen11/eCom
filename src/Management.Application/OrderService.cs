using Domain.Entities;

namespace Management.Application;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task<Order> CreateOrder(Order order, CancellationToken cancellationToken = default) =>
        await orderRepository.CreateOrder(order);

    public async Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken = default) =>
        await orderRepository.GetOrderAsync(id);

    public async Task<bool> OrderExistsAsync(int id, CancellationToken cancellationToken = default) =>
        await orderRepository.OrderExistsAsync(id);
}