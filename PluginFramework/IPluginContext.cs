using System;
using System.Collections.Generic;

namespace PluginFramework
{
    public delegate void Notification(string str);
    public interface IPluginContext
    {
        event Notification INotify;
        Dictionary<string, object> Parameters { get; set; }

        object this[string index]
        {
            get;
            set;
        }
        void Log (string message);
    }
}
