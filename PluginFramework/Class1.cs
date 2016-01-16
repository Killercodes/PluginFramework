using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginFramework
{


    public class Class1:IPlugin
    {
        PluginContext pc = new PluginContext();
       
        [CallOut("PreMain")]
        public void Main()
        {
            PluginContext pc = new PluginContext();
            pc.Add(1, new Class1());
            PluginFramework.Invoker inv = new Invoker(pc);
            inv.RunPlugin("PostMain");

        }




        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public int Order
        {
            get { throw new NotImplementedException(); }
        }

        public void Execute (PluginContext pluginContext)
        {
            throw new NotImplementedException();
        }
    }
}
