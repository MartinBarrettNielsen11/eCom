namespace Management.Persistence;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class DependencyInjeciton
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
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}