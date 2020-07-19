using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Destiny.Core.Modules
{
   public class StartupModulesOptions
    {


        public ICollection<IAppModule> Modules { get; } = new List<IAppModule>();

        public void DiscoverStartupModules() => DiscoverStartupModules(Assembly.GetEntryAssembly()!);


        public void AddModule<T>(T module) where T : IAppModule
    => Modules.Add(module);
        public void DiscoverStartupModules(params Assembly[] assemblies)
        {
       

            foreach (var type in assemblies.SelectMany(a => a.ExportedTypes))
            {
                if (typeof(IAppModule).IsAssignableFrom(type))
                {
                    var instance = Activate(type);
                    Modules.Add(instance);
                }
              
            }
        }

        private IAppModule Activate(Type type)
        {
            try
            {
                return (IAppModule)Activator.CreateInstance(type)!;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create instance for {nameof(IAppModule)} type '{type.Name}'.", ex);
            }
        }
    }
}
