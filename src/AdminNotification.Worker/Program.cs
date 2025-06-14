using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;

namespace AdminNotification.Worker;

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
                services.AddMassTransit(x =>
                {
                    x.SetKebabCaseEndpointNameFormatter();
                    var entryAssembly = Assembly.GetEntryAssembly();
                    x.AddConsumers(entryAssembly);
                    x.UsingRabbitMq((busRegistrationContext, cfg) =>
                    {
                        cfg.ConfigureEndpoints(busRegistrationContext);
                        cfg.ReceiveEndpoint("order-created", e =>
                        {
                            e.ConfigureConsumer<OrderCreatedNotification>(busRegistrationContext);
                        });
                    });
                });
            });
}