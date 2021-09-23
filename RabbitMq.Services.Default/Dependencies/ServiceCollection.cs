using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMq.Common.Commons;
using RabbitMq.Services.Contract.Contracts;
using RabbitMq.Services.Default.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Services.Default.Dependencies
{
    public static class ServiceCollection
    {
        public static void AddConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMessageBrokerService, MessageBrokerService>();
            services.AddScoped<IRabbitMqService, RabbitMqService>();
        }
    }
}
