namespace Management.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        CosmosSettings cosmosSettings = config.GetSection("Cosmos")
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
