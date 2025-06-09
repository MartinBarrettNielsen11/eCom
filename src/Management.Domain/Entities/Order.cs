using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Domain.Entities;

public class Order
{
    //[JsonPropertyName("id")] // System.Text.Json
    [JsonProperty("id")]     // Newtonsoft
    public int Id { get; set; }
    public Guid OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }

    public Customer Customer { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }

    public string Discriminator { get; set; } = "Order";
}