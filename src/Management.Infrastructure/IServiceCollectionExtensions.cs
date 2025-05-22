using System.Diagnostics.CodeAnalysis;
using Management.Application;
using Management.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<OrderContext>(options => options
            .UseSqlServer(connectionString));

        services.AddScoped<IOrderContext>(sp => sp.GetRequiredService<OrderContext>());

        return services;
    }
    
}
