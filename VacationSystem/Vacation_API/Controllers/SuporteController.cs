using Vacation_API.Email;
using Vacation_API.Messaging;
using Vacation_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Vacation_API.Controllers
{
    [Route("api/Suporte")]
    [ApiController]
    public class SuporteController : ControllerBase
    {
        private readonly ISqsConsumer _consumer;
        private APIResponse _response;
       
        public SuporteController(ISqsConsumer consumer)
        {
            _consumer = consumer;
            _response = new();
            
        }
        [HttpGet]
        public async Task<ActionResult<APIResponse>> ConsumeQueue(bool PM, CancellationToken token)
        {
            
            await _consumer.ConsumeQueueAsync(token, PM);
           
            _response.isSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
    }
}
