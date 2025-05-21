using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Management.Application;

public class OrderRepository : IOrderRepository
{
    private readonly OrderContext _context;

    public OrderRepository(OrderContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrder(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> GetOrderAsync(int id) =>
        await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);

    
    public async Task<bool> OrderExistsAsync(int id) => await _context.Orders.AnyAsync(e => e.Id == id);
}