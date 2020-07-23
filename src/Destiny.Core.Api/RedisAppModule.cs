using Destiny.Core.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Destiny.Core.Api
{
    public class RedisAppModule: AppModule
    {
        public override void Configure(ApplicationContext context)
        {
           var app=  context.ServiceProvider.GetRequiredService<Objects<IApplicationBuilder>>();
        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
           
            var con= context.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
             var redis=  con.GetSection("Redis").Value;
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath; //获取项目路径
            var conn = Path.Combine(basePath, redis);
            var redisConn = File.ReadAllText(conn).Trim(); ;
            var csredis = new CSRedis.CSRedisClient(redisConn);
            RedisHelper.Initialization(csredis);

            context.Services.AddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>));
            context.Services.AddSingleton(typeof(IDistributedCache<,>), typeof(DistributedCache<,>));
        }
    }
}
