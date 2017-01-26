using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginFramework
{
    /// <summary>
    /// Interface for plugin, must be implemented
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Name of Plugin
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Order of plugin
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Event on which to execute
        /// </summary>
        String Event { get; }

        /// <summary>
        /// The execute method
        /// </summary>
        /// <param name="pluginContext"></param>
        void Execute(ref IPluginContext pluginContext);

        event Notification INotify;
        
    }
}
