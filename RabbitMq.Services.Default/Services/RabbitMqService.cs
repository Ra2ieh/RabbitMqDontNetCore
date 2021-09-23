using Newtonsoft.Json;
using RabbitMq.Common.Models;
using RabbitMq.Services.Contract.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Services.Default.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IMessageBrokerService _messageBrokerService;
        public RabbitMqService(IMessageBrokerService messageBrokerService)
        {

            _messageBrokerService = messageBrokerService;
        }
        public RabbitMqService()
        {
                
        }
        public async Task GetMessage(MessageModel content)
        {
            var model=_messageBrokerService.GetInstance();
            var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(content));
            model.BasicPublish("", "Direct", false, null, message);
            model.Close();
            throw new NotImplementedException();
        }

    }
}
