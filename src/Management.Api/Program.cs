using System.Configuration;
using Api.Consumer;
using Api.Requests.CreateRequest;
using Domain.Entities;
using Management.Application;
using Management.Persistence;
using MassTransit;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((_, options) =>
    {
        options.ValidateScopes = true;
        options.ValidateOnBuild = true;
    }
);

var tbd = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    //.AddJsonFile("appsettings.json", optional: true)
    .AddUserSecrets<Program>();

IConfiguration config = tbd.Build();


var client = new CosmosClient("https://maazincodes.documents.azure.com:443/",
    "7zKaoq24jzMBSMBdQpYiHokKOhh1LB0TSxQDGBjR9OvdoZLaBZT0O3Nd5YCeZL6gU72SkKE2tvLXACDbPI2tdA==");

var db = client.GetDatabase("test-db");

var container = db.GetContainer("container-1");

// insert temp test obj
var test = new Test()
{
    Name = "test-2"
};

ItemResponse<Test> response = await container.CreateItemAsync(test);

Console.WriteLine($"{response.RequestCharge} RUs for this call");

// have a look at this: https://henriquesd.medium.com/azure-cosmos-db-using-ef-core-with-a-nosql-database-in-a-net-web-api-fce11c5802bd


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
