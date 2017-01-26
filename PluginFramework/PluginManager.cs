using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PluginFramework
{
    public  class PluginManager
    {
        private Dictionary<string, IPlugin> PluginsList = new Dictionary<string, IPlugin>();
        private IPluginContext Context;

        public PluginManager (ref IPluginContext pluginContext)
        {
            Context = pluginContext;

            LoadPlugins(AppDomain.CurrentDomain.BaseDirectory + "\\plugins");
        }

        private void LoadPlugins(string folder)
        {
            foreach (string  dll in Directory.GetFiles(folder, "*.dll"))
            {
                Assembly assembly = Assembly.LoadFrom(dll);
                //find every type in each assembly that implements IPlugin
                Type[] allTypes = assembly.GetTypes()
                    .Where(type => type.GetInterface(typeof(IPlugin).Name) != null)
                    .ToArray<Type>();
                foreach (Type type in allTypes)
                {                  
                   // if (type.GetInterface("IPlugin") == typeof(IPlugin) )
                        //typeof(IPlugin).IsAssignableFrom(type))
                    {
                        IPlugin plu = Activator.CreateInstance(type) as IPlugin;
                        PluginsList[type.FullName] = (plu);
                        //PluginsByEvent[plugin.Event] = (plugin);
                    }
                }
                assembly = null;
            }
        }

        public IPluginContext ExecutePlugin (String pluginName)
        {
            IList<IPlugin> executionList = new List<IPlugin>();
            foreach (var p in PluginsList)
            {
                if (p.Value.Name.ToLower().Equals(pluginName))
                    executionList.Add(p.Value);
            }

            executionList = executionList.OrderBy(o => o.Order).ToList();

            foreach (IPlugin item in executionList)
            {
                item.Execute(ref Context);
            }

           // PluginsList[pluginName].Execute(ref Context);
            return Context;
        }
        public IPluginContext RegisterEvent (String eventName)
        {
            IList<IPlugin> executionList = new List<IPlugin>();
            foreach (var p in PluginsList)
            {
                if (p.Value.Event.ToLower().Equals(eventName.ToLower()))
                    executionList.Add(p.Value);
            }

            executionList = executionList.OrderBy(o => o.Order).ToList();

            foreach (IPlugin item in executionList)
            {
                item.Execute(ref Context);
            }

            // PluginsList[pluginName].Execute(ref Context);
            return Context;
        }
    }

}
