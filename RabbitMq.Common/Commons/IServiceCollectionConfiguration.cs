using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Common.Commons
{
    public interface IServiceCollectionConfiguration
    {
        void Configure(IServiceCollection services,IConfiguration configuration );
    }
}
