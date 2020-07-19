
using Destiny.Core.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Destiny.Core.Api
{
    public class MvcAppModule : IAppModule
    {






    
        public void Configure(ApplicationContext context)
        {
        
            var app = context.Application;
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddControllers();
        }
    }
}
