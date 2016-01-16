using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginFramework
{

    public class PluginContext :IPluginContext
    {
        private Dictionary<int, Object> dictContext;

        public PluginContext ()
        {
            dictContext = new Dictionary<int, Object>();
        }

        public int Count
        {
            get { return dictContext.Count(); }
        }
        public Dictionary<int, Object>.KeyCollection Keys
        {
            get { return dictContext.Keys; }
        }
        public Dictionary<int, Object>.ValueCollection Values
        {
            get { return dictContext.Values; }
        }


        public void Add (int key, Object value)
        {
            dictContext.Add(key, value);
        }

        public bool Remove (int key)
        {
            return dictContext.Remove(key);
        }

        public void MyFunction ()
        {
            //do nothing
        }
        public void Clear ()
        {
            dictContext.Clear();
        }

        public bool ContainsKey (int key)
        {
            return dictContext.ContainsKey(key);
        }

        public bool ContainsValue (Object value)
        {
            return dictContext.ContainsValue(value);
        }


    }
}
