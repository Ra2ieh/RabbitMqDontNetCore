﻿using Microsoft.Extensions.Options;
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
    public class RabbitMqTemporaryService : IRabbitMqService
    {
        private readonly IMessageBrokerService _messageBrokerService;
        private readonly AppConfigs _configs;

        public ServiceType ServiceType => ServiceType.Temporary;



        public RabbitMqTemporaryService(IMessageBrokerService messageBrokerService, IOptions<AppConfigs> options)
        {

            _messageBrokerService = messageBrokerService;
            _configs = options.Value;
        }

        public async  Task SendMessage(MessageModel context)
        {
            var channel = _messageBrokerService.GetInstance();
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(context));
            channel.BasicPublish(exchange: "",
                         routingKey: _configs.TemperaryConfig.RoutingKey,
                         false,
                         basicProperties: null,
                         body: body);
            channel.Close();
        }
    }
}
