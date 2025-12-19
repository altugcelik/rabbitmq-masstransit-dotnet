using MassTransit;
using Microsoft.EntityFrameworkCore;
using Services.Order.Consumers;
using Services.Order.Outbox;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<OrderDbContext>(opt =>
    opt.UseNpgsql("Host=localhost;Port=5432;Database=saga;Username=saga;Password=saga"));

// MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateOrderConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(ctx);
    });
});

// Controllers
builder.Services.AddControllers();

// Outbox Worker
builder.Services.AddHostedService<OutboxPublisherWorker>();

var app = builder.Build();

// ❗ DB CREATE (DEV ONLY)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    db.Database.EnsureCreated();
}

app.MapControllers();

app.Run();
