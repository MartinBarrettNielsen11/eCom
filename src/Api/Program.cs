using Api.Consumer;
using Contracts.Events;
using Contracts.Models;
using DataAccess;
using Domain.Entities;
using Infrastructure;
using MassTransit;
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


app.MapGet("/weatherforecast", async (
        OrderModel model, 
        CancellationToken cancellationToken) =>
    {
        var order = new Order()
        {
            OrderId = model.OrderId,
            CustomerId = 1,
            OrderDate = DateTime.Now,
        };

        /*
        var createdOrder = await _orderService.CreateOrder(order);
        
        var notifyOrderCreated = _publishEndpoint.Publish(new OrderCreated()
            {
                CreatedAt = createdOrder.OrderDate,
                Id = createdOrder.Id,
                OrderId = createdOrder.OrderId,
                TotalAmount = createdOrder.OrderItems.Sum(p => p.Price * p.Quantity)

            }, context =>
            {
                context.Headers.Set("my-custom-header", "value");
                context.TimeToLive = TimeSpan.FromSeconds(30);
            }
        );
        */
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
