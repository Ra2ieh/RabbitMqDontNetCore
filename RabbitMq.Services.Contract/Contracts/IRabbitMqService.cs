using RabbitMq.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Services.Contract.Contracts
{
    public interface IRabbitMqService
    {
        Task SetDirectMessage(MessageModel content);
        Task SetFanoutMessage(MessageModel content);
        Task SetTopicMessage(MessageModel content);
        Task SetHeaderMessage(MessageModel content);
        Task SetAlternativeMessage(MessageModel content);
        Task SetTemperaryMessage(MessageModel content);
        Task SetTTlMessage(MessageModel content);
        Task SetOverFlowMessage(MessageModel content);
    }
}
