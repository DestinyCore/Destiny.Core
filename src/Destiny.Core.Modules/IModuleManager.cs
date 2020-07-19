using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Modules
{
  public  interface IModuleManager
    {

        void ConfigureServices(ConfigureServicesContext context);

         void Configure(ApplicationContext context);
    }
}
