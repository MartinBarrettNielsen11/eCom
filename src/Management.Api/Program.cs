using Api.Requests.CreateRequest;
using Management.Application;
using Management.Infrastructure.Messaging;
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

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddUserSecrets<Program>() // Ensures secret.json is included
    .Build();

var cosmosClientOptions = new CosmosClientOptions()
{
    ConnectionMode = ConnectionMode.Direct,
    MaxRetryAttemptsOnRateLimitedRequests = 0 // disable retries
};

var endpoint = config["Cosmos:Endpoint"];
var primaryKey = config["Cosmos:PrimaryKey"];
var databaseId = config["Cosmos:Database"];
var containerId = config["Cosmos:Container"];

var client = new CosmosClient(endpoint, primaryKey, cosmosClientOptions);

var db = client.GetDatabase(databaseId);

builder.Services.AddOpenApi();

builder.Services
    .AddApplication()
    .AddMessaging()
    .AddPersistence(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/orders", async(
    [FromBody] CreateOrderRequest request, IMediator mediator, ISendEndpointProvider s,
    CancellationToken cancellationToken) =>
    {
        var ss = await s.GetSendEndpoint(new Uri("queue:order-creation"));
        await ss.Send(request);
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

// add optional indexing policy, TTL, etc. here.
var containerProperties = new ContainerProperties
{
    Id = containerId,
    PartitionKeyPath = "/id",
    // Optional indexingPolicy, default TTL, etc. can go here
};

// Try create container with specified config
// Verify setup n azure portal and set autoscale/manual RU's accordingly.
await db.CreateContainerIfNotExistsAsync(
    containerProperties,
    throughput: 1000 // autoscale or manual RU/s
);

app.Run();


public class ProgramApi;
