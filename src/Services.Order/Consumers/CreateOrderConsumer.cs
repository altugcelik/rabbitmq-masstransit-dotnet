using System.Text.Json;
using MassTransit;
using Services.Order.Outbox;
using Shared.Contracts.Commands;
using Shared.Contracts.Events;

namespace Services.Order.Consumers;

public class CreateOrderConsumer : IConsumer<CreateOrderCommand>
{
    private readonly OrderDbContext _db;

    public CreateOrderConsumer(OrderDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<CreateOrderCommand> context)
    {
        var evt = new OrderCreatedEvent(context.Message.OrderId);

        _db.OutboxMessages.Add(new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = typeof(OrderCreatedEvent).AssemblyQualifiedName!,
            Payload = JsonSerializer.Serialize(evt),
            OccurredOn = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
    }
}
