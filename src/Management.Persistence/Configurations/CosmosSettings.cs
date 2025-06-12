namespace Management.Persistence.Configurations;

public class CosmosSettings
{
    public string Endpoint { get; init; } = default!;
    public string PrimaryKey { get; init; } = default!;
    public string DatabaseId { get; init; } = default!;
    public string ContainerId { get; init; } = default!;
}