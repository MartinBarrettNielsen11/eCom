using Contracts.Models;
using Management.Application.CommandHandlers;

namespace Api.Requests.CreateRequest;

public class CreateOrderRequest(ICollection<OrderItemModel> orderItems)
{
    public int CustomerId { get; init; }
    public ICollection<OrderItemModel> OrderItems { get; init; } = orderItems;

    public CreateOrderCommand ToCommand() => new(CustomerId, OrderItems);
}