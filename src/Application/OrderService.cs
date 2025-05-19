using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Service;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task<Order> CreateOrder(Order order)
    {
        return await orderRepository.CreateOrder(order);
    }

    public async Task<Order> GetOrderAsync(int id)
    {
        return await orderRepository.GetOrderAsync(id);
    }

    public async Task<bool> OrderExistsAsync(int id)
    {
        return await orderRepository.OrderExistsAsync(id);
    }
}