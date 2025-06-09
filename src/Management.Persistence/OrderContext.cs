using Domain.Entities;
using Management.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Management.Persistence;

public class OrderContext(DbContextOptions<OrderContext> options) : 
    DbContext(options), IOrderContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    // To-Do: Remove this suppression of synchronous I/O for Cosmos db. Should not stay lng temr.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(w => w.Ignore(CosmosEventId.SyncNotSupported));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAutoscaleThroughput(1000);
        modelBuilder.HasDefaultContainer("container-1");

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToContainer("container-1");
            entity.HasPartitionKey(x => x.Id);
            entity.HasKey(x => x.Id);
            entity.HasDiscriminator<string>(x => x.Discriminator).HasValue("Customer");
        });
        
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToContainer("container-1");
            entity.HasPartitionKey(x => x.Id);
            entity.HasKey(x => x.Id);
            entity.HasDiscriminator<string>(x => x.Discriminator).HasValue("Order");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToContainer("container-1");
            entity.HasPartitionKey(x => x.Id);
            entity.HasKey(x => x.Id);
            entity.HasDiscriminator<string>(x => x.Discriminator).HasValue("OrderItem");
        });
        

        /** TO-DO **/
        // have a look at this: https://henriquesd.medium.com/azure-cosmos-db-using-ef-core-with-a-nosql-database-in-a-net-web-api-fce11c5802bd}
    }
}

