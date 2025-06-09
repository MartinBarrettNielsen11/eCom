using Newtonsoft.Json;

namespace Domain.Entities;

public class Customer
{
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public ICollection<Order> Orders { get; set; }

    public string Discriminator { get; set; } = "Customer";
    public string PartitionKeyPath { get; set; } = "id";
}