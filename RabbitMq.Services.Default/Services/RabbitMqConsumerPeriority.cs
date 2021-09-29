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
    public class RabbitMqConsumerPeriority : IRabbitMqService
    {
        private readonly IMessageBrokerService _messageBrokerService;
        private readonly AppConfigs _configs;

        public ServiceType ServiceType => ServiceType.ConsumerPriority;

        public RabbitMqConsumerPeriority(IMessageBrokerService messageBrokerService, IOptions<AppConfigs> options)
        {

            _messageBrokerService = messageBrokerService;
            _configs = options.Value;
        }

        public async Task SendMessage(MessageModel context)
        {
            var channel = _messageBrokerService.GetInstance();

            for (int i = 1; i <= 20; i++)
            {
                var message = JsonConvert.SerializeObject(context);
                var body = Encoding.UTF8.GetBytes(message + " " + i);
                var properties = channel.CreateBasicProperties();
                properties.SetPersistent(true);
                channel.BasicPublish(_configs.ConsumerPeriorityConfig.ExchangeName, _configs.ConsumerPeriorityConfig.RoutingKey, false, properties, body);

            }
            channel.Close();
        }
    }
}
