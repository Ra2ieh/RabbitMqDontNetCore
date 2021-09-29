using RabbitMq.Common.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Common.Models
{
    public class MessageModel
    {
        public string Name { get; set; }
        public string NationalCode { get; set; }
        public string RoutingKey { get; set; }
        public byte ServiceType { get; set; }
    }
    public class BaseResult
    {
        public string Error { get; set; }
    }
}
