using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Management.Application;

public class OrderRepository(IOrderContext context) : IOrderRepository
{
    public async Task<Order> CreateOrder(Order order)
    {
        context.Orders.Add(order);
        return order;
    }

    public async Task<Order?> GetOrderAsync(int id) =>
        await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);
    
    public async Task<bool> OrderExistsAsync(int id) => await context.Orders.AnyAsync(e => e.Id == id);
}