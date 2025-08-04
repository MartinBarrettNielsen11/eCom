namespace Management.Persistence.Configurations;

public class CosmosSettings
{
    public string Endpoint { get; init; } = null!;
    public string PrimaryKey { get; init; } = null!;
    public string Database { get; init; } = null!;
    public string Container { get; init; } = null!;
}