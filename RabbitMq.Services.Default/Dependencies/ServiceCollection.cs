using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMq.Common;
using RabbitMq.Services.Contract.Contracts;
using RabbitMq.Services.Default.Services;
using RabbitMq.Services.Default.Services.HostedService;

namespace RabbitMq.Services.Default.Dependencies
{
    public static class ServiceCollection
    {
        public static void AddConfigure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ServiceFactory>();
            services.AddSingleton<IMessageBrokerService, MessageBrokerService>();
            services.AddScoped<IRabbitMqService, RabbitMqDirectService>();
            services.AddScoped<IRabbitMqService, RabbitMqFanoutService>();
            services.AddScoped<IRabbitMqService, RabbitMqTopicService>();
            services.AddScoped<IRabbitMqService, RabbitMqHeaderService>();
            services.AddScoped<IRabbitMqService, RabbitMqAlternativeService>();
            services.AddScoped<IRabbitMqService, RabbitMqOverFlowService>();
            services.AddScoped<IRabbitMqService, RabbitMqTTlService>();
            services.AddScoped<IRabbitMqService, RabbitMqTemporaryService>();
            services.AddScoped<IRabbitMqService, RabbitMqPriorityService>();
            services.AddScoped<IRabbitMqService, RabbitMqConsumerPeriority>();
            services.AddScoped<IRabbitUseCaseService, RabbitUseCaseService>();

            services.AddHostedService<TTLHostedService>();
            services.AddOptions();
            services.Configure<AppConfigs>(configuration.GetSection("AppConfigs"));
     
        }
    }
}
