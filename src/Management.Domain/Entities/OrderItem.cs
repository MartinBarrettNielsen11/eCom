using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Domain.Entities;

public class OrderItem
{
    [JsonPropertyName("id")] // System.Text.Json
    [JsonProperty("id")]     // Newtonsoft
    public Guid Id { get; set; }
    public int ProductId { get; set; }

    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public Order Order { get; set; }

    public string Discriminator { get; set; } = "OrderItem";
}