using Common.Application.Extensions;
using Common.Domain.Events.Notification;
using MassTransit;
using Notification.Worker;
using Notification.Worker.Consumers;

var builder = Host.CreateApplicationBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendVerificationEmailRequestEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqConnStr = configuration.GetConnectionString("RabbitMq");
        cfg.Host(rabbitMqConnStr);

        #region SendVerificationEmailRequestEventConsumer
        var sendVerificationEmailQueueName = MessageBrokerExtensions.GetQueueName<SendVerificationEmailRequestEvent>();
        cfg.ReceiveEndpoint(sendVerificationEmailQueueName, e =>
        {
            e.ConfigureConsumer<SendVerificationEmailRequestEventConsumer>(context);
            
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
