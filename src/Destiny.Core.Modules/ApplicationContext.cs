using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Modules
{
  public  class ApplicationContext
    {


         public ApplicationContext(IServiceProvider serviceProvider) {
      
            ServiceProvider = serviceProvider;

        }
        //public IApplicationBuilder Application { get; }

        public IServiceProvider ServiceProvider { get; }
    }
}
