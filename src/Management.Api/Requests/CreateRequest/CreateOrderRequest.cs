using Contracts.Models;
using Management.Application.CommandHandlers;

namespace Api.Requests.CreateRequest;

public class CreateOrderRequest
{
    public string Phone { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public int CustomerId { get; init; }
    public DateTime OrderDate { get; init; }
    public Guid OrderId { get; init; } = new();
    public List<OrderItemModel> OrderItems { get; init; } = new();

    public CreateOrderCommand ToCommand() =>
        new(OrderId, CustomerId, OrderDate);
}