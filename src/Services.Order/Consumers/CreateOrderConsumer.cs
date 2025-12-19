using MassTransit;
using Shared.Contracts.Commands;
using Shared.Contracts.Events;

namespace Services.Order.Consumers;

public class CreateOrderConsumer : IConsumer<CreateOrderCommand>
{
    public async Task Consume(ConsumeContext<CreateOrderCommand> context)
    {
        await context.Publish(
            new OrderCreatedEvent(context.Message.OrderId));
    }
}
