using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Services.Contract.Contracts
{
    public interface IMessageBrokerService
    {
        IModel GetInstance();
        void Setting();

    }
}
