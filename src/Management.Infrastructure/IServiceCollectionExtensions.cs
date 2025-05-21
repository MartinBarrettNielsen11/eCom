using System.Diagnostics.CodeAnalysis;
using DataAccess;
using Management.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString) =>
        services.AddDbContext<OrderContext>(options => options
            .UseSqlServer(connectionString));
}
