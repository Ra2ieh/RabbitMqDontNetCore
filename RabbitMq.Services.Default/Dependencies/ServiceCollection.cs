using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMq.Common;
using RabbitMq.Services.Contract.Contracts;
using RabbitMq.Services.Default.Services;


namespace RabbitMq.Services.Default.Dependencies
{
    public static class ServiceCollection
    {
        public static void AddConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMessageBrokerService, MessageBrokerService>();
            services.AddScoped<IRabbitMqService, RabbitMqService>();
            services.AddOptions();
            services.Configure<AppConfigs>(configuration.GetSection("AppConfigs"));
     
        }
    }
}
