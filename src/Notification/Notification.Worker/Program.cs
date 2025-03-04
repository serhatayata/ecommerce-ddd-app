using MassTransit;
using Notification.Worker;
using Notification.Worker.Consumers;

var builder = Host.CreateApplicationBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendVerificationEmailIntegrationEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqConnStr = configuration.GetConnectionString("RabbitMq");
        cfg.Host(rabbitMqConnStr);

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddLogging();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
