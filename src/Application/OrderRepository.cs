using Domain.Entities;

namespace Service;

public class OrderRepository : IOrderRepository
{
    public Task<Order> CreateOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> OrderExistsAsync(int id)
    {
        throw new NotImplementedException();
    }
}