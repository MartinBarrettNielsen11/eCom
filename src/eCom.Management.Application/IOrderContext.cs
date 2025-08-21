using Domain.Customers;

namespace Management.Application;

public interface IOrderContext
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Domain.Orders.Order> Orders { get; set; }
    DbSet<OrderItem> OrderItems { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}
