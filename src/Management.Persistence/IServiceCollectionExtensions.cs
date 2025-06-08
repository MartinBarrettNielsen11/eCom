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
            .UseCosmos(
                accountEndpoint: "https://maazincodes.documents.azure.com:443/",
                accountKey: "7zKaoq24jzMBSMBdQpYiHokKOhh1LB0TSxQDGBjR9OvdoZLaBZT0O3Nd5YCeZL6gU72SkKE2tvLXACDbPI2tdA==",
                databaseName: "test-db")
        );
        
        services.AddScoped<IOrderContext>(sp => sp.GetRequiredService<OrderContext>());
        
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
    
}