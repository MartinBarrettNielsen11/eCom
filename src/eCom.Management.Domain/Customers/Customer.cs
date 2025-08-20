using System.Text.Json.Serialization;
using Domain.Orders;
using Newtonsoft.Json;

namespace Domain.Customers;

public class Customer
{
    [JsonPropertyName("id")] // System.Text.Json
    [JsonProperty("id")]     // Newtonsoft
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public ICollection<Order> Orders { get; set; }

    public string Discriminator { get; set; } = "Customer";
}
