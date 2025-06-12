namespace Management.Persistence.Configurations;

public class CosmosSettings
{
    public string Endpoint { get; init; } = default!;
    public string PrimaryKey { get; init; } = default!;
    public string Database { get; init; } = default!;
    public string Container { get; init; } = default!;
}