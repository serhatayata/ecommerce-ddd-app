using Common.Application.Extensions;
using Common.Domain.Events.Notification;
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

        #region SendVerificationEmailIntegrationEventConsumer
        var sendVerificationEmailQueueName = MessageBrokerExtensions.GetQueueName<SendVerificationEmailRequestEvent>();
        cfg.ReceiveEndpoint(sendVerificationEmailQueueName, e =>
        {
            e.ConfigureConsumer<SendVerificationEmailIntegrationEventConsumer>(context);
            
            var sendVerificationEmailExchangeName = MessageBrokerExtensions.GetExchangeName<SendVerificationEmailRequestEvent>();
            e.Bind(sendVerificationEmailExchangeName, x =>
            {
                x.ExchangeType = "fanout";
                x.Durable = true;
            });
        });
        #endregion
    });
});

builder.Services.AddLogging();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
