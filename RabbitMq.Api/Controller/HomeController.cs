using Microsoft.AspNetCore.Mvc;
using RabbitMq.Common.Models;
using RabbitMq.Services.Contract.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMq.Api.Controller
{
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly IRabbitMqService _service;
        public HomeController(IRabbitMqService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> SendDirectMessage([FromBody] MessageModel message)
        {
          await   _service.SetDirectMessage(message);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> SetFanoutMessage([FromBody] MessageModel message)
        {
          await   _service.SetFanoutMessage(message);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> SetTopicMessage([FromBody]MessageModel message)
        {
          await   _service.SetTopicMessage(message);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> SetHeaderMessage([FromBody] MessageModel message)
        {
          await   _service.SetHeaderMessage(message);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> SetAlternativeMessage([FromBody] MessageModel message)
        {
            await _service.SetAlternativeMessage(message);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> SetTemperaryMessage([FromBody] MessageModel message)
        {
          await   _service.SetTemperaryMessage(message);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> SetTTlMessage([FromBody] MessageModel message)
        {
            await _service.SetTTlMessage(message);
            return Ok();
        }

    }
}
