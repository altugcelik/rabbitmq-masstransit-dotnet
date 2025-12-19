using MassTransit;
using Services.Stock.Consumers;

Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ReserveStockConsumer>();

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
    })
    .Build()
    .Run();
