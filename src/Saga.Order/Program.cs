using MassTransit;
using Saga.Order;

Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.AddSagaStateMachine<OrderStateMachine, OrderState>()
             .InMemoryRepository(); // Prod’da PostgreSQL

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
