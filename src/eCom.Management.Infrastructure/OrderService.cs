namespace Management.Infrastructure;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task<Order> CreateOrder(Order order, CancellationToken cancellationToken = default) =>
        await orderRepository.CreateOrder(order, cancellationToken);

    public async Task<Order?> GetOrderAsync(Guid id, CancellationToken cancellationToken = default) =>
        await orderRepository.GetOrderAsync(id);

    public async Task<bool> OrderExistsAsync(Guid id, CancellationToken cancellationToken = default) =>
        await orderRepository.OrderExistsAsync(id);
}