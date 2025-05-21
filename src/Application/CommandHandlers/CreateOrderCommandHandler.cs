namespace Service.CommandHandlers;

public record CreateOrderCommand(Guid OrderId, int CustomerId, DateTime OrderDate);
public class CreateOrderCommandHandler
{
    
}