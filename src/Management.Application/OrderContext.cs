using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Service;

public class OrderContext(DbContextOptions<OrderContext> options) : 
    DbContext(options), IOrderContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}