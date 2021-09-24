using Microsoft.Extensions.Options;
using RabbitMq.Common;
using RabbitMq.Services.Contract.Contracts;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Services.Default.Services
{
    public class MessageBrokerService : IMessageBrokerService
    {
        private ConnectionFactory _connectionFactory;
        private readonly AppConfigs _configs;
        private IConnection _connection;
        public MessageBrokerService( IOptions<AppConfigs> configs)
        {
            _configs = configs.Value;
            Setting();
        }


        public void Setting()
        {
             _connectionFactory ??= new ConnectionFactory() { HostName = "localhost" };
            _connection = _connectionFactory.CreateConnection();
            using var channel = GetInstance();
            //direct
            #region Direct
            //direct
            channel.QueueDeclare(queue: _configs.DirectConfig.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            #endregion

            //fanout
            #region Fanout

            channel.ExchangeDeclare(_configs.FanoutConfig.ExchangeName, ExchangeType.Fanout, durable: false, autoDelete: false, arguments: null);
            for (int i = 1; i < 6; i++)
            {
                channel.QueueDeclare(queue:$"{ _configs.FanoutConfig.QueueName}{i}", durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueBind(queue: $"{ _configs.FanoutConfig.QueueName}{i}",exchange: _configs.FanoutConfig.ExchangeName, "", null);
            }

            #endregion

            //topic 
            #region Topic
            channel.ExchangeDeclare(exchange:_configs.TopicConfig.ExchangeName,ExchangeType.Topic,durable: false,autoDelete: false,arguments: null);
            channel.QueueDeclare(queue: $"{_configs.TopicConfig.QueueName}1",durable: false,exclusive: false,autoDelete: false,arguments: null);
            channel.QueueDeclare(queue: $"{_configs.TopicConfig.QueueName}2",durable: false,exclusive: false,autoDelete: false,arguments: null);

            channel.QueueBind(queue: $"{_configs.TopicConfig.QueueName}1",exchange: _configs.TopicConfig.ExchangeName, routingKey: "*.test.*", null);
            channel.QueueBind(queue: $"{_configs.TopicConfig.QueueName}2", exchange: _configs.TopicConfig.ExchangeName,routingKey: "*.test.x", null);
            #endregion


            //header 
            #region Header


            var propQ1 = new Dictionary<string, object> { { "x-match", "any" },
                                                          { "type", "header" },
                                                          { "color", "green" }};

            var propQ2 = new Dictionary<string, object> { { "x-match", "any" },
                                                          { "count", "ten" } };

            var propQ3 = new Dictionary<string, object> { { "x-match", "all" },
                                                          { "type", "header" },
                                                          { "color", "green" }};
            channel.ExchangeDeclare(exchange:_configs.HeaderConfig.ExchangeName,ExchangeType.Headers,durable: false,autoDelete: false,arguments: null);
            channel.QueueDeclare(queue: $"{_configs.HeaderConfig.QueueName}1",durable: false,exclusive: false,autoDelete: false,arguments: null);
            channel.QueueDeclare(queue: $"{_configs.HeaderConfig.QueueName}2",durable: false,exclusive: false,autoDelete: false,arguments: null);
            channel.QueueDeclare(queue: $"{_configs.HeaderConfig.QueueName}3",durable: false, exclusive: false,autoDelete: false,arguments: null);

            channel.QueueBind(queue: $"{_configs.HeaderConfig.QueueName}1", exchange: _configs.HeaderConfig.ExchangeName, "", propQ1);
            channel.QueueBind(queue: $"{_configs.HeaderConfig.QueueName}2", exchange: _configs.HeaderConfig.ExchangeName, "", propQ2);
            channel.QueueBind(queue: $"{_configs.HeaderConfig.QueueName}3", exchange: _configs.HeaderConfig.ExchangeName, "", propQ3);
            #endregion

            //alternative exchange 
            #region Alternative
            var alternativeArg = new Dictionary<string, object> { { "alternate-exchange", "my-ae" } };

            channel.ExchangeDeclare(_configs.AltMainConfig.ExchangeName, ExchangeType.Direct, false, false, alternativeArg);
            channel.ExchangeDeclare(_configs.AltRetryConfig.ExchangeName, ExchangeType.Fanout, false, false, null);
            //channel.QueueDeclare(_configs.AltMainConfig.QueueName);//exclusive is true 
            channel.QueueDeclare(_configs.AltMainConfig.QueueName,false,exclusive:false,autoDelete:false);
            channel.QueueBind(_configs.AltMainConfig.QueueName, _configs.AltMainConfig.ExchangeName, "key1");
            channel.QueueDeclare(_configs.AltRetryConfig.QueueName, false, exclusive: false, autoDelete: false);
            channel.QueueBind(_configs.AltRetryConfig.QueueName, _configs.AltRetryConfig.ExchangeName, "");
            #endregion
            //temperary queue
            #region Temperary
            //temperary 

            var arg = new Dictionary<string, object> { { "x-expires", _configs.TemperaryConfig.ExpireTime }, { "x-message-ttl", _configs.TemperaryConfig.TTl } };
            channel.QueueDeclare(queue: _configs.TemperaryConfig.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            #endregion
            //queue with ttl
            #region TTl
            //main exchange 
            channel.ExchangeDeclare(exchange: _configs.TTLMainConfig.ExchangeName, ExchangeType.Direct, durable: false, autoDelete: false, arguments: null);
            channel.ExchangeDeclare(exchange: _configs.TTlRetryConfig.ExchangeName, ExchangeType.Direct, durable: false, autoDelete: false, arguments: null);
            var ttlMainQueueArg = new Dictionary<string, object> { { "x-dead-letter-exchange", _configs.TTlRetryConfig.ExchangeName },
                                                                   { "x-dead-letter-routing-key", _configs.TTlRetryConfig.RoutingKey } };
            channel.QueueDeclare(queue: _configs.TTLMainConfig.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: ttlMainQueueArg);

            channel.QueueBind(queue: _configs.TTLMainConfig.QueueName,exchange:_configs.TTLMainConfig.ExchangeName,routingKey:_configs.TTLMainConfig.RoutingKey);

            //retry exchange 

            var ttlRetryQueueArg = new Dictionary<string, object> { 
                                                            { "x-dead-letter-exchange", _configs.TTlRetryConfig.ExchangeName },
                                                            { "x-dead-letter-routing-key", _configs.TTlRetryConfig.RoutingKey },
                                                            { "x-message-ttl", _configs.TTlRetryConfig.TTl } };
            channel.QueueDeclare(queue: _configs.TTlRetryConfig.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: ttlRetryQueueArg);

            channel.QueueBind(queue: _configs.TTlRetryConfig.QueueName, exchange: _configs.TTlRetryConfig.ExchangeName, routingKey: _configs.TTLMainConfig.RoutingKey);

            #endregion
            channel.Close();
            channel.Dispose();
        }

        public IModel GetInstance()
        {
            var model = _connection.CreateModel();
            return model;
        }
    }
}
