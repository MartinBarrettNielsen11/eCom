using Api.Consumer;
using Api.Requests.CreateRequest;
using Management.Application;
using Management.Persistence;
using MassTransit;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((_, options) =>
    {
        options.ValidateScopes = true;
        options.ValidateOnBuild = true;
    }
);
    
builder.Services.AddOpenApi();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer, OrderCreatedConsumerDefinition>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

var connectionString = builder.Configuration.GetConnectionString("OrdersContext");

builder.Services
    .AddApplication()
    .AddPersistence(connectionString);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/orders", async(
    [FromBody] CreateOrderRequest request, IMediator mediator,
    CancellationToken cancellationToken) =>
    {
        var result = await mediator.Send(request.ToCommand(), cancellationToken);
        return result.Result;
    })
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest);

app.MapOpenApi().AllowAnonymous();

app.MapScalarApiReference(options =>
{
    options.Title = "Tbd.Api";
    options.Theme = ScalarTheme.Laserwave;
}).AllowAnonymous();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    serviceScope.ServiceProvider.GetService<OrderContext>()!.Database.EnsureCreated();
}

app.Run();


public class ProgramApi;
