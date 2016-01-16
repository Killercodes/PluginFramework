using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginFramework
{

    
    public interface IPlugin
    {
        String Name { get; }
        int Order { get; }

        void Execute(PluginContext pluginContext);
        
    }
}
