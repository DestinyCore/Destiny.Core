using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Destiny.Core.Modules
{
   public class StartupModulesOptions
    {

 
     
        public ICollection<IAppModule> Modules { get; } = new List<IAppModule>();
        private List<IAppModule> _source;

        public StartupModulesOptions()
        {
            _source = new List<IAppModule>();
        }

        public void DiscoverStartupModules<T>() where T : IAppModule => DiscoverStartupModules<T>(Assembly.GetEntryAssembly()!);


        public void AddModule<T>(T module) where T : IAppModule
    => Modules.Add(module);

        private  List<Assembly> LoadAssemblies(string folderPath, SearchOption searchOption)
        {
            return GetAssemblyFiles(folderPath, searchOption)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                .ToList();
        }
        private IEnumerable<string> GetAssemblyFiles(string folderPath, SearchOption searchOption)
        {
            return Directory
                .EnumerateFiles(folderPath, "*.*", searchOption)
                .Where(s => s.EndsWith(".dll") || s.EndsWith(".exe"));
        }
        public void DiscoverStartupModules<T>(params Assembly[] assemblies) where T : IAppModule
        {

            //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            //var aaa= LoadAssemblies(basePath, SearchOption.AllDirectories).SelectMany(o=>o.GetTypes()).Where(type=>type.Name== "ConsoleApp1");
            foreach (var type in assemblies.SelectMany(a => a.ExportedTypes))
            {
                if (typeof(IAppModule).IsAssignableFrom(type))
                {
                    var instance = Activate(type);
                    _source.Add(instance);
                }
              
            }

           var module=  _source.FirstOrDefault(o => o.GetType() == typeof(T));
            if (module == null)
            {
                throw new Exception($"类型为“{typeof(T).FullName}”的模块实例无法找到");
            }
            Modules.Add(module);
           var dependeds= module.GetDependedTypes();

            foreach (var dependType in dependeds)
            {
                var dependModule = _source.Find(m => m.GetType() == dependType);
                if (dependModule == null)
                {
                    throw new Exception($"加载模块{module.GetType().FullName}时无法找到依赖模块{dependType.FullName}");
                }
                if (!Modules.Contains(dependModule))
                {
                    Modules.Add(dependModule);
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
