using Contracts.Models;
using Management.Application.CommandHandlers;

namespace Api.Requests.CreateRequest;

public class CreateOrderRequest
{
    public CreateOrderRequest(ICollection<OrderItemModel> orderItems)
    {
        OrderItems = orderItems;
    }

    public string Phone { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public int CustomerId { get; init; }
    public int OrderId { get; init; } = new();
    public ICollection<OrderItemModel> OrderItems { get; init; }

    public CreateOrderCommand ToCommand() =>
        new(OrderItems);
}