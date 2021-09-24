using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMq.Common;
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
        private readonly AppConfigs _configs;
        public RabbitMqService(IMessageBrokerService messageBrokerService,IOptions<AppConfigs> options)
        {

            _messageBrokerService = messageBrokerService;
            _configs = options.Value;
        }
        public async Task SetDirectMessage(MessageModel content)
        {
            var channel=_messageBrokerService.GetInstance();
            var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(content));
            channel.BasicPublish("", _configs.DirectConfig.RoutingKey, false, null, message);
            channel.Close();

        }

        public async  Task SetFanoutMessage(MessageModel content)
        {
            var channel = _messageBrokerService.GetInstance();
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(content));

            channel.BasicPublish(exchange:_configs.FanoutConfig.ExchangeName,
                                 routingKey: "",
                                 false,
                                 basicProperties: null,
                                 body: body);
            channel.Close();
        }

        public async  Task SetTopicMessage(MessageModel content)
        {
            var channel = _messageBrokerService.GetInstance();
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(content));
            channel.BasicPublish(exchange:_configs.TopicConfig.ExchangeName,
                         routingKey:content.RoutingKey,
                         false,
                         basicProperties: null,
                         body: body);
            channel.Close();

        }

        public async  Task SetHeaderMessage(MessageModel content)
        {
            var channel = _messageBrokerService.GetInstance();
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(content));
            var prop = channel.CreateBasicProperties();
            prop.Headers = new Dictionary<string, object> { { "type", "header" } };
            channel.BasicPublish(exchange: _configs.HeaderConfig.ExchangeName,
                         routingKey: "",
                         false,
                         basicProperties: prop,
                         body: body);
        }

        public async Task SetAlternativeMessage(MessageModel content)
        {
            var channel = _messageBrokerService.GetInstance();
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(content));

            channel.BasicPublish(exchange: _configs.AltMainConfig.ExchangeName,
                                         routingKey: "key2",
                                         false,
                                         basicProperties: null,
                                         body: body);
        }

        public Task SetTemperaryMessage(MessageModel content)
        {
            throw new NotImplementedException();
        }

        public Task SetTTlMessage(MessageModel content)
        {
            throw new NotImplementedException();
        }
    }
}
