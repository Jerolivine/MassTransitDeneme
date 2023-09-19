using MassTransit;
using MassTransit.Middleware;
using MassTransit.RabbitMqTransport.Configuration;
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

                    cfg.CustomReceiveEndpoint("message-consumer", e =>
                    {
                        e.ConfigureConsumer<MessageConsumer>(context);
                        e.ConfigureError(x => x.UseFilter(new LogExceptionFilter()));
                    });

                });
            });
        }

        private static void CustomReceiveEndpoint(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator cfg, Action<IRabbitMqReceiveEndpointConfigurator> action)
        {
            cfg.ReceiveEndpoint("message-consumer", action);
        }

        private static Action<IRabbitMqReceiveEndpointConfigurator> NewMethod1(IBusRegistrationContext context)
        {
            return e =>
            {
                e.ConfigureConsumer<MessageConsumer>(context);
                e.ConfigureError(x => x.UseFilter(new LogExceptionFilter()));
            };
        }
    }
}


public class LogExceptionFilter :
        IFilter<ExceptionReceiveContext>
{
    //private readonly ILogger<LogExceptionFilter> _logger;

    //public LogExceptionFilter(ILogger<LogExceptionFilter> logger)
    //{
    //    _logger = logger;
    //}

    void IProbeSite.Probe(ProbeContext context)
    {

    }

    async Task IFilter<ExceptionReceiveContext>.Send(ExceptionReceiveContext context, IPipe<ExceptionReceiveContext> next)
    {
        try
        {
            LogException(context);
        }
        finally
        {
            await next.Send(context).ConfigureAwait(false);
        }

    }

    private void LogException(ExceptionReceiveContext context)
    {
        //context.GetMessageId
        if (context.Exception is not null)
        {
            Guid? messageId;
            Guid? requestId;
            string[] messageTypes = null;

            if (context.TryGetPayload(out ConsumeContext consumeContext))
            {
                messageId = consumeContext.MessageId;
                requestId = consumeContext.RequestId;
                messageTypes = consumeContext.SupportedMessageTypes.ToArray();
            }
            else
            {
                messageId = context.GetMessageId();
                requestId = context.GetRequestId();
            }

            //_logger.LogError(context.Exception, "An exception occurred while processing the message.");
        }
    }
}

public static class MassTransitRabbitMQExtension
{
    public static void CustomReceiveEndpoint(this IRabbitMqBusFactoryConfigurator cfg, string queueName, Action<IRabbitMqReceiveEndpointConfigurator> action)
    {
        cfg.ReceiveEndpoint(queueName, action);
    }
}