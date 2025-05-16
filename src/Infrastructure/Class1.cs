using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
    {
        return services
            .AddScoped<, CommunicationContext>()
            .AddDbContext<CommunicationContext>(options => options
                .UseSqlServer(connectionString));
    }
}
