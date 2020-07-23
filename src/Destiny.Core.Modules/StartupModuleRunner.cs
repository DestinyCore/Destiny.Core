using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Modules
{
   public class StartupModuleRunner
    {

        private readonly StartupModulesOptions _options;



        public StartupModuleRunner(IServiceCollection services, StartupModulesOptions options)
        {
            _options = options;
            services.AddSingleton(this);


        }

        public void ConfigureServices(IServiceCollection services)
        {
            var ctx = new ConfigureServicesContext(services);
            services.AddSingleton(ctx);
            foreach (var cfg in _options.Modules)
            {
                cfg.ConfigureServices(ctx);
            }
        }

        public void Configure(IServiceProvider service)
        {
            using var scope = service.CreateScope();
            var ctx = new ApplicationContext(scope.ServiceProvider);
            foreach (var cfg in _options.Modules)
            {
                cfg.Configure(ctx);
            }
        }
    }
}
