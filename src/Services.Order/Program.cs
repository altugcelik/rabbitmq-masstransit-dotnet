using MassTransit;
using Microsoft.EntityFrameworkCore;
using Services.Order.Consumers;
using Services.Order.Outbox;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<OrderDbContext>(opt =>
            opt.UseNpgsql(
                "Host=localhost;Port=5432;Database=saga;Username=saga;Password=saga"));
    
        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateOrderConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("appuser");
                    h.Password("apppassword");
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });

        services.AddHostedService<DatabaseInitializer>();
        services.AddHostedService<OutboxPublisherWorker>();
    })
    .Build()
    .Run();
