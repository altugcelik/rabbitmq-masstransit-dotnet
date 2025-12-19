namespace Shared.Contracts.Commands;

public record CreateOrderCommand(Guid OrderId, decimal Amount);
