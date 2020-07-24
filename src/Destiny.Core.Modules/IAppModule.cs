using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Modules
{

    /// <summary>
    /// 模块接口
    /// </summary>
    public interface IAppModule
    {

        void ConfigureServices(ConfigureServicesContext context);


        void Configure(ApplicationContext context);

        Type[] GetDependedTypes(Type moduleType = null);


    }
}
