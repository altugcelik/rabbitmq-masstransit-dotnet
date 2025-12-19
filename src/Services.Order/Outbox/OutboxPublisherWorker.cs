using System.Text.Json;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Services.Order.Outbox;

public class OutboxPublisherWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<OutboxPublisherWorker> _logger;

    public OutboxPublisherWorker(
        IServiceScopeFactory scopeFactory,
        IPublishEndpoint publishEndpoint,
        ILogger<OutboxPublisherWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Outbox Publisher started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();

                var messages = await db.OutboxMessages
                    .Where(x => !x.Processed)
                    .OrderBy(x => x.OccurredOn)
                    .Take(10)
                    .ToListAsync(stoppingToken);

                foreach (var message in messages)
                {
                    var messageType = Type.GetType(message.Type);
                    if (messageType == null)
                    {
                        _logger.LogWarning("Unknown message type: {Type}", message.Type);
                        message.Processed = true;
                        continue;
                    }

                    var payload = JsonSerializer.Deserialize(
                        message.Payload, messageType);

                    await _publishEndpoint.Publish(payload!, messageType, stoppingToken);

                    message.Processed = true;

                    _logger.LogInformation("Outbox message published: {Id}", message.Id);
                }

                await db.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Outbox publishing failed");
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
