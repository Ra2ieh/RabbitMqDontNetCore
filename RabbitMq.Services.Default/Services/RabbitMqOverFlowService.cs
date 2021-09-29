using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMq.Common;
using RabbitMq.Common.Commons;
using RabbitMq.Common.Models;
using RabbitMq.Services.Contract.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Services.Default.Services
{
    public class RabbitMqOverFlowService : IRabbitMqService
    {
        private readonly IMessageBrokerService _messageBrokerService;
        private readonly AppConfigs _configs;

        public ServiceType ServiceType => ServiceType.Overflow;


        public RabbitMqOverFlowService(IMessageBrokerService messageBrokerService, IOptions<AppConfigs> options)
        {

            _messageBrokerService = messageBrokerService;
            _configs = options.Value;
        }

        public async Task SendMessage(MessageModel context)
        {
            var channel = _messageBrokerService.GetInstance();

            for (int i = 0; i < 15; i++)
            {
                var body = Encoding.UTF8.GetBytes($"{i}--{JsonConvert.SerializeObject(context)}");
                channel.BasicPublish(exchange: "",
             routingKey: "OveflowQueue",
             false,
             basicProperties: null,
             body: body);
            }

            channel.Close();
        }
    }
}
