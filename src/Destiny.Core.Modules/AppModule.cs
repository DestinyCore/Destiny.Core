using System;
using System.Collections.Generic;
using System.Text;

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

       
    }
}
