using RabbitMq.Common.Commons;
using RabbitMq.Common.Models;
using RabbitMq.Services.Contract.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Services.Default.Services
{
    public class RabbitUseCaseService: IRabbitUseCaseService
    {
        private readonly ServiceFactory _serviceFactory;
        public RabbitUseCaseService(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task<BaseResult> SendMessage(MessageModel content)
        {
            var result = new BaseResult();
            var service = _serviceFactory.GetInstance((ServiceType)content.ServiceType);
            if(service is null)
            {
                result.Error = "not found";
                return result;
            }
            await service.SendMessage(content);
            return result;
        }

    }
}
