using MassTransit;
using Shared.Contracts.Commands;
using Shared.Contracts.Events;

namespace Saga.Order;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public State WaitingForPayment { get; private set; } = null!;
    public State WaitingForStock { get; private set; } = null!;

    public Event<OrderCreatedEvent> OrderCreated { get; private set; } = null!;
    public Event<PaymentCompletedEvent> PaymentCompleted { get; private set; } = null!;
    public Event<StockReservedEvent> StockReserved { get; private set; } = null!;

    public OrderStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => OrderCreated, x => x.CorrelateById(m => m.Message.OrderId));
        Event(() => PaymentCompleted, x => x.CorrelateById(m => m.Message.OrderId));
        Event(() => StockReserved, x => x.CorrelateById(m => m.Message.OrderId));

        Initially(
            When(OrderCreated)
                .TransitionTo(WaitingForPayment)
                .Send(new Uri("queue:process-payment"),
                    ctx => new ProcessPaymentCommand(ctx.Saga.CorrelationId))
        );

        During(WaitingForPayment,
            When(PaymentCompleted)
                .TransitionTo(WaitingForStock)
                .Send(new Uri("queue:reserve-stock"),
                    ctx => new ReserveStockCommand(ctx.Saga.CorrelationId))
        );

        During(WaitingForStock,
            When(StockReserved)
                .Publish(ctx =>
                    new OrderCompletedEvent(ctx.Saga.CorrelationId))
                .Finalize()
        );

        SetCompletedWhenFinalized();
    }
}
