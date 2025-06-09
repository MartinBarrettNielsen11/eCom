using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Domain.Entities;

public class OrderItem
{
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }

    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public Order Order { get; set; }

    public string Discriminator { get; set; } = "OrderItem";
}