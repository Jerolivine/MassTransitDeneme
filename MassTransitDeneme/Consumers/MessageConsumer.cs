using MassTransit;

namespace MassTransitDeneme.Consumers
{
    public class MessageConsumerRequest
    {
        public string Message { get; set; }
    }

    public class MessageConsumer : IConsumer<MessageConsumerRequest>
    {
        readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MessageConsumerRequest> context)
        {
            if(string.IsNullOrEmpty(conte))
            _logger.LogInformation("Received Message: {Message}", context.Message.Message);

            return Task.CompletedTask;
        }
    }
}
