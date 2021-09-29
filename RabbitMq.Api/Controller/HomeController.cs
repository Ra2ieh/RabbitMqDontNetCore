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
        private readonly IRabbitUseCaseService _service;
        public HomeController(IRabbitUseCaseService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageModel message)
        {
          await   _service.SendMessage(message);
            return Ok();
        }


    }
}
