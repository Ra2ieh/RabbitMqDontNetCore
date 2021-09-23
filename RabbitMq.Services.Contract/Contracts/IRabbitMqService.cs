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
        Task GetMessage(MessageModel content);
    }
}
