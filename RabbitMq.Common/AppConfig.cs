using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Common
{
    public class AppConfigs
    {
        public RabbitConfigs DirectConfig { get; set; }
        public RabbitConfigs FanoutConfig { get; set; }
        public RabbitConfigs TopicConfig { get; set; }
        public RabbitConfigs HeaderConfig { get; set; }
        public RabbitConfigs TemperaryConfig { get; set; }
        public RabbitConfigs TTLMainConfig { get; set; }
        public RabbitConfigs TTlRetryConfig { get; set; }
        public RabbitConfigs AltRetryConfig { get; set; }
        public RabbitConfigs AltMainConfig { get; set; }
    }

    public class RabbitConfigs
    {
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public string ExchangeType { get; set; }
        public int TTl { get; set; }
        public int ExpireTime { get; set; }
    }
}
