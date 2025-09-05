namespace Management.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddMessaging()
            .AddApplication()
            .AddScoped<IDateTimeProvider, DateTimeProvider>();
}
