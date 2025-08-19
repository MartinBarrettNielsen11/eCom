namespace Management.Application.Mappers;

[Mapper]
public partial class OrderMapper
{
    public partial Order MapToOrder(CreateOrderCommand command);
    
    // optional (for now)
    public partial OrderItem MapToOrderItem(OrderItemModel model);
}
