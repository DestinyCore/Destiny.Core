
using Destiny.Core.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Destiny.Core.Api
{
    [DependsOn(typeof(Test1))]
    public class MvcAppModule : AppModule
    {






    
        public override void Configure(ApplicationContext context)
        {
        
            var app = context.ServiceProvider.GetRequiredService<IObjects<IApplicationBuilder>>()?.Value;
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public override void  ConfigureServices(ConfigureServicesContext context)
        {
            var logger = context.Services.BuildServiceProvider().GetService<ILoggerFactory>();
            logger.CreateLogger<MvcAppModule>().LogInformation("MvcAppModule已注册");
            context.Services.AddControllers();
        }
    }



    [DependsOn(typeof(Test2), typeof(Test3),typeof(RedisAppModule))]
    public class Test1 : AppModule
    {
    

        public override void Configure(ApplicationContext context)
        {
       
            base.Configure(context);
        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var logger = context.Services.BuildServiceProvider().GetService<ILoggerFactory>();
            logger.CreateLogger<Test1>().LogInformation("Test1已注册");
            base.ConfigureServices(context);
        }
    }

    [DependsOn(typeof(Test3))]
    public class Test2 : AppModule
    {
       
        public override void Configure(ApplicationContext context)
        {
            
            base.Configure(context);
        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var logger = context.Services.BuildServiceProvider().GetService<ILoggerFactory>();
            logger.CreateLogger<Test2>().LogInformation("Test2已注册");
            base.ConfigureServices(context);
        }
    }

    public class Test3 : AppModule
    {

        public override void Configure(ApplicationContext context)
        {
            base.Configure(context);
        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var logger = context.Services.BuildServiceProvider().GetService<ILoggerFactory>();
            logger.CreateLogger<Test3>().LogInformation("Test3已注册");
            base.ConfigureServices(context);
        }
    }


    public class Test4 : AppModule
    {

        public override void Configure(ApplicationContext context)
        {
            base.Configure(context);
        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var logger = context.Services.BuildServiceProvider().GetService<ILoggerFactory>();
            logger.CreateLogger<Test4>().LogInformation("Test4已注册");
            base.ConfigureServices(context);
        }
    }
}
