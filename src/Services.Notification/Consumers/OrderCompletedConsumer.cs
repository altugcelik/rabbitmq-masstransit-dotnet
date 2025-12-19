using MassTransit;
using Shared.Contracts.Events;

namespace Services.Notification.Consumers;

public class OrderCompletedConsumer : IConsumer<OrderCompletedEvent>
{
    public Task Consume(ConsumeContext<OrderCompletedEvent> context)
    {
        Console.WriteLine($"📧 Order completed: {context.Message.OrderId}");

        return Task.CompletedTask;
    }
}
