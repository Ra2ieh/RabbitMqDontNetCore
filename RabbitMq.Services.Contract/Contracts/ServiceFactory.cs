using RabbitMq.Common.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Services.Contract.Contracts
{
    public class ServiceFactory
    {
        private readonly IEnumerable<IRabbitMqService> _rabbitMqServices;
        public ServiceFactory(IEnumerable<IRabbitMqService> rabbitMqServices)
        {
            _rabbitMqServices = rabbitMqServices;
        }

        public IRabbitMqService GetInstance(ServiceType type)
        {
            return _rabbitMqServices.FirstOrDefault(e => e.ServiceType == type);
        }
    }
}
