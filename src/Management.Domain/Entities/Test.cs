using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Domain.Entities;

public class Test
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
}