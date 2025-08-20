namespace Management.Application.Order.Mappers;

[Mapper]
public partial class OrderMapper
{
    public partial Domain.Orders.Order MapToOrder(CreateOrderCommand command);
    
    // optional (for now)
    public partial OrderItem MapToOrderItem(OrderItemModel model);
}
