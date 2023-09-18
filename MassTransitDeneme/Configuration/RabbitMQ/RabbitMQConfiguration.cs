using MassTransit;
using MassTransitDeneme.Configuration.RabbitMQ.Model;
using MassTransitDeneme.Consumers;

namespace MassTransitDeneme.Configuration.RabbitMQ
{
    public static class RabbitMQConfiguration
    {
        public static void AddMassTransitWithRabbitMqTransport(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqOptions = configuration.GetSection("RabbitMQ").Get<RabbitMQOptions>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<MessageConsumer>(); 
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(rabbitMqOptions.Host), h => { });

                    cfg.ReceiveEndpoint("message-consumer", e =>
                    {
                        e.ConfigureConsumer<MessageConsumer>(context);
                    });
                });
            });
        }
    }
}
