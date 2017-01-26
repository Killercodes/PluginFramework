using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginFramework.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class CallOutAttribute :Attribute
    {
        public IPluginContext context { get; set; }

        PluginManager invoker;// = new PluginManager();
        public string InvokeOn {get;set;}
        //public CallOut (string pluginName)
        //{
        //    invoker.ExecutePlugin(pluginName);
        //}
        //public CallOut (string pluginName, IPluginContext context)
        //{
        //    invoker.ExecutePlugin(pluginName);
        //}

        public CallOutAttribute (Type context)
        {
            //IPluginContext ipc = (IPluginContext)(Activator.CreateInstance(context));
            //invoker = new PluginManager(ipc);
            invoker.ExecutePlugin(InvokeOn);
        }
    }
}
