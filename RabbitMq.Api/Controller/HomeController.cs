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
        public async Task<IActionResult> GetMessage(MessageModel message)
        {
          await   _service.GetMessage(message);
            return Ok();
        }
    }
}
