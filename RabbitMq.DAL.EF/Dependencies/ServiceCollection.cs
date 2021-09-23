using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMq.Common.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.DAL.EF.Dependencies
{
    public static class ServiceCollection
    {
        public static void AddConfigure(this IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
