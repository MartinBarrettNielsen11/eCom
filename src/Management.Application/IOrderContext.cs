namespace Management.Application;

public interface IOrderContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}