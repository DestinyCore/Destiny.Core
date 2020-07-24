using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Destiny.Core.Modules
{
    public abstract class AppModule : IAppModule
    {
        public virtual void ConfigureServices(ConfigureServicesContext context)
        {

        }
        public virtual void Configure(ApplicationContext context)
        {
          
        }

        private ConfigureServicesContext _configureServicesContext;
        protected internal ConfigureServicesContext ConfigureServicesContext
        {
            get
            {
             
                return _configureServicesContext;
            }
            internal set => _configureServicesContext = value;
        }

        protected void Configure<TOptions>(Action<TOptions> configureOptions)
         where TOptions : class
        {
            ConfigureServicesContext.Services.Configure(configureOptions);
        }

        protected void Configure<TOptions>(string name, Action<TOptions> configureOptions)
            where TOptions : class
        {
            ConfigureServicesContext.Services.Configure(name, configureOptions);
        }

        protected void Configure<TOptions>(IConfiguration configuration)
            where TOptions : class
        {
            ConfigureServicesContext.Services.Configure<TOptions>(configuration);
        }

        protected void Configure<TOptions>(IConfiguration configuration, Action<BinderOptions> configureBinder)
            where TOptions : class
        {
            ConfigureServicesContext.Services.Configure<TOptions>(configuration, configureBinder);
        }

        protected void Configure<TOptions>(string name, IConfiguration configuration)
            where TOptions : class
        {
            ConfigureServicesContext.Services.Configure<TOptions>(name, configuration);
        }


     
        public   Type[] GetDependedTypes(Type moduleType = null)
        {
            if (moduleType == null)
            {
                moduleType = GetType();
            }
          
            var dependedTypes = moduleType.GetCustomAttributes().OfType<IDependedTypesProvider>().ToArray();
            if (dependedTypes.Length == 0)
            {
                return new Type[0];
            }
            List<Type> dependList = new List<Type>();
            foreach (var dependedType in dependedTypes)
            {
                var dependeds=  dependedType.GetDependedTypes();
                if (dependeds.Length == 0) {
                    continue;
                }
                dependList.AddRange(dependeds);

                foreach (Type type in dependeds)
                {
                    dependList.AddRange(GetDependedTypes(type));
                }

            }
            return dependList.Distinct().ToArray();
        }
    }
}
