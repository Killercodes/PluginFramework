using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginFramework
{
    [AttributeUsage(AttributeTargets.All)]
    public class CallOut :Attribute
    {
        Invoker invoker = new Invoker();
        public CallOut (string pluginName)
        {
            invoker.RunPlugin(pluginName);
        }
        public CallOut (string pluginName, PluginContext context)
        {
            invoker.RunPlugin(pluginName);
        }
    }
}
