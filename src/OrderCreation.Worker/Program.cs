namespace OrderCreation.Worker;

public class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {

                services.AddScoped<IOrderRepository, OrderRepository>();
                services.AddScoped<IOrderService, OrderService>();
                    
                services.AddMassTransit(x =>
                {
                    var entryAssembly = Assembly.GetEntryAssembly();
                    x.AddConsumers(entryAssembly);
                    x.UsingRabbitMq((context, cfg) => { cfg.ConfigureEndpoints(context); });
                });
            });
}