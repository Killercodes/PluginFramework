using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Configuration;

namespace PluginFramework
{
    public  class PluginManager
    {
 
        private Dictionary<string, IPlugin> PluginsList = new Dictionary<string, IPlugin>();
        private IPluginContext _context;

        public PluginManager (ref IPluginContext pluginContext)
        {
            _context = pluginContext; 
            string pluginDirectory = string.Format("{0}plugins",AppDomain.CurrentDomain.BaseDirectory);
            if(!Directory.Exists(pluginDirectory))
            {
                pluginDirectory = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["pluginfolder"]);
            }
            _context.Log(string.Format("Loading plugins from [{0}]", pluginDirectory));
            LoadPlugins(pluginDirectory);
            
        }

        private void LoadPlugins(string folder)
        {
            string[] dllInFolder = Directory.GetFiles(folder, "*.dll");
            
            _context.Log(string.Format("No of files in folder [{0}]", dllInFolder.Count().ToString()));
            foreach (string  dll in dllInFolder)
            {
                _context.Log(string.Format("Scanning [{0}] for plugins",Path.GetFileName(dll)));
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
                        _context.Log(string.Format("+ Adding [{0}] to repository", type.Name));
                        IPlugin plu = Activator.CreateInstance(type) as IPlugin;
                        PluginsList[type.Name] = (plu);
                    }
                }
                assembly = null;
            }
        }

        public IPluginContext ExecutePlugin (String pluginName)
        {
            _context.Log(string.Format("Searching for [{0}] in repository", pluginName));
            IList<IPlugin> executionList = new List<IPlugin>();
            foreach (var p in PluginsList)
            {
                if (p.Value.GetType().Name.ToLower().Equals(pluginName))
                    executionList.Add(p.Value);
            }

            executionList = executionList.OrderBy(o => o.Order).ToList();
            _context.Log(string.Format("- Number of plugins found [{0}] in execution list",executionList.Count));
            foreach (IPlugin item in executionList)
            {
                _context.Log(string.Format("- Executing [{0}]",item.GetType().Name));
                item.Execute(ref _context);
            }

           // PluginsList[pluginName].Execute(ref _context);
            return _context;
        }
        public IPluginContext RegisterEvent (String eventName)
        {
            _context.Log(string.Format("Searching for registered event [{0}] in repository", eventName));
            IList<IPlugin> executionList = new List<IPlugin>();
            foreach (var p in PluginsList)
            {
                if (p.Value.Event.ToLower().Equals(eventName.ToLower()))
                    executionList.Add(p.Value);
            }

            executionList = executionList.OrderBy(o => o.Order).ToList();
            _context.Log(string.Format("- Number of plugins found [{0}] in execution list", executionList.Count));
            foreach (IPlugin item in executionList)
            {
                _context.Log(string.Format("- Executing [{0}] for registered event [{1}] ", item.GetType().Name, eventName));
                item.Execute(ref _context);
            }

            // PluginsList[pluginName].Execute(ref _context);
            return _context;
        }
    }

}
