using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PluginFramework
{
    public  class Invoker
    {
        private Dictionary<string, IPlugin> Plugins = new Dictionary<string, IPlugin>();
        private PluginContext Context;

        public Invoker (PluginContext pluginContext)
        {
            Context = pluginContext;
            LoadPlugins("~\\plugins");
        }
        public Invoker () { }

        private void LoadPlugins(string folder)
        {
            foreach (string  dll in Directory.GetFiles(folder, "*.dll"))
            {
                Assembly assembly = Assembly.LoadFrom(dll);
                //find every type in each assembly that implements IPlugin
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetInterface("IPlugin") == typeof(IPlugin))
                    {
                        IPlugin plugin = Activator.CreateInstance(type) as IPlugin;
                        Plugins[plugin.Name] = (plugin);
                    }
                }
            }
        }

        public PluginContext RunPlugin (String name)
        {
            Plugins[name].Execute(Context);
            return Context;
        }
    }

}
