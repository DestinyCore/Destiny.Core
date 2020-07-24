using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Destiny.Core.Modules
{
 public static   class IServiceCollectionExtensions
    {

        public static IServiceCollection AddApplication<T>(this IServiceCollection services) where T : IAppModule =>
             AddApplication(services, options => options.DiscoverStartupModules<T>());



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
            var obj = new Objects<IApplicationBuilder>();
            services.Add(ServiceDescriptor.Singleton(typeof(Objects<IApplicationBuilder>), obj));
            services.Add(ServiceDescriptor.Singleton(typeof(IObjects<IApplicationBuilder>), obj));
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
            builder.ApplicationServices.GetRequiredService<Objects<IApplicationBuilder>>().Value = builder;
            var runner = builder.ApplicationServices.GetRequiredService<StartupModuleRunner>();
            runner.Configure(builder.ApplicationServices);
            return builder;
        }
    }
}
