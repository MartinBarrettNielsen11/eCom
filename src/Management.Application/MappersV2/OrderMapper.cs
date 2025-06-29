using Contracts.Models;
using Domain.Entities;
using Management.Application.CommandHandlers;
using Riok.Mapperly.Abstractions;

namespace Management.Application.MappersV2;

[Mapper]
public partial class OrderMapper
{
    public partial Order MapToOrder(CreateOrderCommand command);
    
    // optional (for now)
    public partial OrderItem MapToOrderItem(OrderItemModel model);
}