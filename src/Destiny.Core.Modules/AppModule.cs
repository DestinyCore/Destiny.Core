using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
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
    }
}
