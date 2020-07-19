using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Modules
{
  public  class ConfigureServicesContext
    {

        public ConfigureServicesContext(IServiceCollection services)
        {
            Services = services;
   
        }
        public IServiceCollection Services { get; }

    }
}
