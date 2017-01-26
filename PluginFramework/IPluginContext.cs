using System;

namespace PluginFramework
{
    public delegate void Notification(string str);
    public interface IPluginContext
    {
        event Notification INotify;
        System.Collections.Generic.Dictionary<string, object> Parameters { get; set; }

        object this[string index]
        {
            get;
            set;
        }
    }
}
