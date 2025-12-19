using MassTransit;
using Shared.Contracts.Commands;
using Shared.Contracts.Events;

namespace Services.Stock.Consumers;

public class ReserveStockConsumer : IConsumer<ReserveStockCommand>
{
    public async Task Consume(ConsumeContext<ReserveStockCommand> context)
    {
        await context.Publish(
            new StockReservedEvent(context.Message.OrderId));
    }
}
