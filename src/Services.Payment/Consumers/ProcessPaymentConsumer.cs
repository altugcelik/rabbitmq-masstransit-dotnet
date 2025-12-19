using MassTransit;
using Shared.Contracts.Commands;
using Shared.Contracts.Events;

namespace Services.Payment.Consumers;

public class ProcessPaymentConsumer : IConsumer<ProcessPaymentCommand>
{
    public async Task Consume(ConsumeContext<ProcessPaymentCommand> context)
    {
        // Simüle hata (Retry + DLQ için)
        if (Random.Shared.Next(1, 4) == 1)
            throw new Exception("Payment provider timeout");

        await context.Publish(
            new PaymentCompletedEvent(context.Message.OrderId));
    }
}
