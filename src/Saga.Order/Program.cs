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
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });
    })
    .Build()
    .Run();
