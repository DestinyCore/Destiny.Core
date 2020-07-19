using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Modules
{
  public  class ApplicationContext
    {


         public ApplicationContext(IApplicationBuilder application, IServiceProvider serviceProvider) {
            Application = application;
            ServiceProvider = serviceProvider;

        }
        public IApplicationBuilder Application { get; }

        public IServiceProvider ServiceProvider { get; }
    }
}
