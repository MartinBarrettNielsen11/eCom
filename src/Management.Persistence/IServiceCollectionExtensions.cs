using System.Diagnostics.CodeAnalysis;
using Management.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Management.Persistence;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<OrderContext>(options => options
            .UseSqlServer(connectionString));

        services.AddScoped<IOrderContext>(sp => sp.GetRequiredService<OrderContext>());
        
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
    
}