using System;
namespace PluginFramework
{
    interface IPluginContext
    {
        void Add (int key, object value);
        void Clear ();
        bool ContainsKey (int key);
        bool ContainsValue (object value);
        int Count { get; }
        System.Collections.Generic.Dictionary<int, object>.KeyCollection Keys { get; }
        void MyFunction ();
        bool Remove (int key);
        System.Collections.Generic.Dictionary<int, object>.ValueCollection Values { get; }
    }
}
