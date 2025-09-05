using Api.Infrastructure;
using Api.Requests.CreateRequest;
using Management.Infrastructure;
using Management.Persistence;
using Management.SharedKernel.Results;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((_, options) =>
    {
        options.ValidateScopes = true;
        options.ValidateOnBuild = true;
    }
);

// to do: since switching to mac - be sure to handle the secret.json file
IConfigurationRoot config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddUserSecrets<Program>()
    .Build();

CosmosClientOptions clientOptions = new CosmosClientOptions()
{
    ConnectionMode = ConnectionMode.Direct,
    MaxRetryAttemptsOnRateLimitedRequests = 0 // disable retries
};

var endpoint = config["Cosmos:Endpoint"];
var primaryKey = config["Cosmos:PrimaryKey"];
var databaseId = config["Cosmos:Database"];
var containerId = config["Cosmos:Container"];

CosmosClient client = new CosmosClient(endpoint, primaryKey, clientOptions);

Database? database = client.GetDatabase(databaseId);

builder.Services.AddOpenApi();

builder.Services
    .AddServices()
    .AddPersistence(builder.Configuration);

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/orders", async (
    [FromBody] CreateOrderRequest request,
    IMediator mediator,
    CancellationToken cancellationToken) =>
    {
        Result<Guid> result = await mediator.Send(request.ToCommand(), cancellationToken);
        return result.Match(Results.NoContent, CustomResults.Problem);
    })
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest);

app.MapOpenApi().AllowAnonymous();

app.MapScalarApiReference(options =>
{
    options.Title = "eCom.WebApi";
    options.Theme = ScalarTheme.Laserwave;
}).AllowAnonymous();

ContainerProperties containerProperties = new ContainerProperties
{
    Id = containerId,
    PartitionKeyPath = "/id",
    // Optional indexingPolicy, default TTL, etc. can go here
};

await database.CreateContainerIfNotExistsAsync(containerProperties);

client.Dispose();

await app.RunAsync();


// REMARK: Required for functional and integration tests to work.
namespace Api
{
    public partial class ProgramApi;
}
