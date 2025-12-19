using Microsoft.EntityFrameworkCore;

namespace Services.Order.Outbox;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options) { }

    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OutboxMessage>()
            .HasKey(x => x.Id);

        base.OnModelCreating(modelBuilder);
    }
}
