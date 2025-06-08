using Domain.Entities;
using Management.Application;
using Microsoft.EntityFrameworkCore;

namespace Management.Persistence;

public class OrderContext(DbContextOptions<OrderContext> options) : 
    DbContext(options), IOrderContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAutoscaleThroughput(1000);
        modelBuilder.HasDefaultContainer("container-1");
        
        /** TO-DO **/
        // have a look at this: https://henriquesd.medium.com/azure-cosmos-db-using-ef-core-with-a-nosql-database-in-a-net-web-api-fce11c5802bd
        //  have a look at this: https://www.youtube.com/watch?v=ihFpgzDowcM&t=189s
    }
}

