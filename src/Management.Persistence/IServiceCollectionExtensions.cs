using System.Diagnostics.CodeAnalysis;
using Management.Application;
using Management.Persistence.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Management.Persistence;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var cosmosSettings = config.GetSection("Cosmos")
            .Get<CosmosSettings>() ?? throw new InvalidOperationException("Cosmos config missing");
        
        services.AddSingleton(cosmosSettings);
        
        services.AddDbContext<OrderContext>(options => options
            .UseCosmos(
                accountEndpoint: cosmosSettings.Endpoint,
                accountKey: cosmosSettings.PrimaryKey,
                databaseName: cosmosSettings.Database)
        );
        
        services.AddScoped<IOrderContext>(sp => sp.GetRequiredService<OrderContext>());
        
        // Handle potential circular dependency for this another time
        /*
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();
        */
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}