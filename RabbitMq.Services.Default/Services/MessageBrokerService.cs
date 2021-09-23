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
        private IConnection _connection;
        public MessageBrokerService()
        {
            Setting();
        }
        public IModel GetInstance()
        {
            var model = _connection.CreateModel();
            return model;
        }

        public void Setting()
        {
             _connectionFactory ??= new ConnectionFactory() { HostName = "localhost" };
            _connection = _connectionFactory.CreateConnection();
            using var channel = GetInstance();

            channel.ExchangeDeclare("RejisterExchnge",ExchangeType.Direct,durable:false,autoDelete:true);
            var arg = new Dictionary<string, object> { { "x-expires", 20000 }, { "x-message-ttl", 10000 } };
            channel.QueueDeclare(queue: "Direct",
             durable: false,
             exclusive: false,
             autoDelete: false,
             arguments: null);
            channel.Close();
            channel.Dispose();
        }
    }
}
