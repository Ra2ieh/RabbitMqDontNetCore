using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMqPriorityConsmer3
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.BasicQos(0, 1, false);
                    var consumer = new QueueingBasicConsumer(channel);
                    IDictionary<string, object> consumerArgs = new Dictionary<string, object> { { "x-periority", "3" } };
                    channel.BasicConsume("ConsumerPeriorityQueue", false, "", consumerArgs, consumer);
                    Console.WriteLine(" [*] Waiting for messages. " +
                                      "To exit press CTRL+C");
                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                        channel.BasicAck(ea.DeliveryTag, false);
                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}
