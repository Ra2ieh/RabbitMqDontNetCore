using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Common.Commons
{
    public enum ServiceType:byte
    {
        Direct=1,
        Fanout=2,
        Topic = 3,
        Header=4,
        Alternative=5,
        Temporary=6,
        TTL=7,
        Overflow=8,
        Priority=9,
        ConsumerPriority=10


    }
}
