using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMq.Common;
using RabbitMq.Common.Models;
using RabbitMq.Services.Contract.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMq.Services.Default.Services.HostedService
{
    public class TTLHostedService : IHostedService
    {
        private readonly IMessageBrokerService _messageBrokerService;
        private EventingBasicConsumer _consumer;
        private readonly AppConfigs _configs;
        private IModel _model;

        public TTLHostedService(IMessageBrokerService messageBrokerService, IOptions<AppConfigs> options)
        {
            _messageBrokerService = messageBrokerService;
            _configs = options.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var startedTime = DateTime.Now;

            _model = _messageBrokerService.GetInstance();
            _model.BasicQos(prefetchSize: 0, prefetchCount: 1, global: true);
            AddConsumerToQueue();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Adds the consumer to queue.
        /// </summary>
        private void AddConsumerToQueue()
        {

            _consumer = new EventingBasicConsumer(_model);

            _consumer.Received += (model, args) =>
            {

                ConsumerOnReceived(model, args);
            };

            _model.BasicConsume(queue: _configs.TTLMainConfig.QueueName, autoAck: false, consumer: _consumer);

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;

        }


        private async void ConsumerOnReceived(object sender, BasicDeliverEventArgs args)
        {
            var message = "";
            try
            {
                message = Encoding.UTF8.GetString(args.Body.ToArray());
                var model = JsonConvert.DeserializeObject<MessageModel>(message);
                    _consumer.Model.BasicNack(deliveryTag: args.DeliveryTag, multiple: false, requeue: false);

                    //_consumer.Model.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                _consumer.Model.BasicNack(deliveryTag: args.DeliveryTag, multiple: false, requeue: false);
            }

        }
    }
}
