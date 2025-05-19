using Api.Consumer;
using Contracts.Events;
using Contracts.Models;
using DataAccess;
using Domain.Entities;
using Infrastructure;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using Service;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

app.UseHttpsRedirection();


app.MapPost("/orders", async (
        [FromBody] OrderModel model, 
        IOrderService orderService,
        IPublishEndpoint publishEndpoint,
        CancellationToken cancellationToken) =>
    {
        var order = new Order()
        {
            OrderId = Guid.NewGuid(),
            CustomerId = 1,
            OrderDate = DateTime.Now,
        };

        var createdOrder = await orderService.CreateOrder(order, cancellationToken);

        await publishEndpoint.Publish(new OrderCreated()
        {
            CreatedAt = createdOrder.OrderDate,
            Id = createdOrder.Id,
            OrderId = createdOrder.OrderId,
            TotalAmount = createdOrder.OrderItems.Sum(p => p.Price * p.Quantity)
        }, context =>
        {
            context.Headers.Set("header-v1", "header-v1-value");
            context.TimeToLive = TimeSpan.FromSeconds(30);
        }, cancellationToken);
    })
    .WithName("GetWeatherForecast");
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
