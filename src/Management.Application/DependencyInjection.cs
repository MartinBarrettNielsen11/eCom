using Management.Application.notSure;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddMediator(opts =>
            {
                opts.ServiceLifetime = ServiceLifetime.Transient;
                opts.Assemblies = [ApplicationAssembly.Get()];
                opts.Namespace = "Mediator.SourceGenerator";
                opts.GenerateTypesAsInternal = true;
            })
            .AddScoped<IDateTimeProvider, DateTimeProvider>();
}
