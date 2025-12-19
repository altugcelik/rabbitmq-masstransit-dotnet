using Microsoft.EntityFrameworkCore;

namespace Services.Order.Outbox;

public class DatabaseInitializer : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DatabaseInitializer(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();

        await db.Database.EnsureCreatedAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
