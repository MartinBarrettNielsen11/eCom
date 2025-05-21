using Api.Consumer;
using AutoMapper;
using Contracts.Mappings;
using Contracts.Models;
using Infrastructure;
using Management.Application;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

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

builder.Services
    .AddApplication();

var connectionString = builder.Configuration.GetConnectionString("OrdersContext");

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddAutoMapper(typeof(OrderProfileMapping));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

app.UseHttpsRedirection();


app.MapPost("/orders", async(
    [FromBody] OrderModel request,
    IMapper mapper,
    ISender sender,
    CancellationToken cancellationToken) =>
    {
        //var order = mapper.Map<Order>(model);

        var result = await sender.Send(request.ToCommand(), cancellationToken);

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
