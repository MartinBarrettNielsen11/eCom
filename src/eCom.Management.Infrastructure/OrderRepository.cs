namespace Management.Infrastructure;

public class OrderRepository(IOrderContext context) : IOrderRepository
{
    public async Task<Order> CreateOrder(Order order, CancellationToken cancellationToken)
    {
        SetIds(order); 
        await context.Orders.AddAsync(order, cancellationToken);
        return order;
    }

    public async Task<Order?> GetOrderAsync(Guid id) =>
        await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);
    
    public async Task<bool> OrderExistsAsync(Guid id) => await context.Orders.AnyAsync(e => e.Id == id);
    
    
    private static void SetIds(Order order)
    {
        if (order.Id == Guid.Empty)
        {
            order.Id = Guid.CreateVersion7();
        }

        foreach (OrderItem item in order.OrderItems)
        {
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.CreateVersion7();
            }
        }
    }
}
