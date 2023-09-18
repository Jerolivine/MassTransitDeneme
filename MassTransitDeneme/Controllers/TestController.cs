using MassTransit;
using MassTransit.Clients;
using MassTransitDeneme.Consumers;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitDeneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //public IRequestClient<MessageConsumerRequest> _messageConsumerClient { get; set; }    
        public IPublishEndpoint _publishEndpoint { get; set; }

        public TestController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<ActionResult> PostMessage([FromBody] string message)
        {

            //_messageConsumerClient.Create(new MessageConsumerRequest() { Message = message });

            await _publishEndpoint.Publish<MessageConsumerRequest>(new MessageConsumerRequest() { Message =  message });

            return Ok();
        }
    }
}
