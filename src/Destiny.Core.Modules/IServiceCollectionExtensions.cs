using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Destiny.Core.Modules
{
 public static   class IServiceCollectionExtensions
    {

        public static IServiceCollection AddApplication(this IServiceCollection services) =>
             AddApplication(services, options => options.DiscoverStartupModules());


        public static IServiceCollection AddApplication(this IServiceCollection services, params Assembly[] assemblies) =>
           AddApplication(services, options => options.DiscoverStartupModules(assemblies));

        public static IServiceCollection AddApplication(this IServiceCollection services, Action<StartupModulesOptions> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var options = new StartupModulesOptions();
            configure(options);
            services.AddSingleton(options);
            if (options.Modules.Count == 0)
            {

                return services;
            }

            var runner = new StartupModuleRunner(services,options);
            runner.ConfigureServices(services);
            return services;

        }


        public static IApplicationBuilder InitializeApplication(this IApplicationBuilder builder)
        {
        
            var runner = builder.ApplicationServices.GetRequiredService<StartupModuleRunner>();
            runner.Configure(builder);
            return builder;
        }
    }
}
