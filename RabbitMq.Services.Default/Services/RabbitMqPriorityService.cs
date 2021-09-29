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
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMq.Services.Default.Services
{
    public class RabbitMqPriorityService : IRabbitMqService
    {
        private readonly IMessageBrokerService _messageBrokerService;
        private readonly AppConfigs _configs;

        public ServiceType ServiceType => ServiceType.Priority;

        public RabbitMqPriorityService(IMessageBrokerService messageBrokerService, IOptions<AppConfigs> options)
        {

            _messageBrokerService = messageBrokerService;
            _configs = options.Value;
        }


        public async Task SendMessage(MessageModel context)
        {
            var channel = _messageBrokerService.GetInstance();

            for (int i = 1; i <= 10; i++)
            {
                var message = JsonConvert.SerializeObject(context);
                var body = Encoding.UTF8.GetBytes(message + " " + i);
                var properties = channel.CreateBasicProperties();
                properties.SetPersistent(true);
                //properties.ContentType = "application/json";
                properties.Priority = Convert.ToByte(i);
                channel.BasicPublish(_configs.PeriorityConfig.ExchangeName, _configs.PeriorityConfig.RoutingKey, false, properties, body);
               
            }
            channel.Close();
        }
    }
}
