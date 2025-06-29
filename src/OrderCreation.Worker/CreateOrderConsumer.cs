using System;
using System.Threading.Tasks;
using Contracts.Models;
using Management.Application;
using MassTransit;

namespace OrderCreation.Worker;

public class CreateOrderConsumer(IOrderService orderService) : IConsumer<OrderModel>
{
    public async Task Consume(ConsumeContext<OrderModel> context)
    {
        Console.WriteLine($"I got a command to create an order: {context.Message}");
        
        // to do: copy over relavent parts from current handler over here.
        
        await Task.CompletedTask;
    }
}