using System;
using System.Collections.Generic;
using System.Text;

namespace Sync.Demo.Fine
{
    public class Singleton
    {
        private static object _syncObj = new object();

        private volatile static Singleton _singleton;

        private Singleton() { }

        public static Singleton GetSingleton()
        {
            if (_syncObj == null)
            {
                lock (_syncObj)
                {
                    if (_syncObj == null)
                    {
                        _singleton = new Singleton();
                    }
                }
            }
            return _singleton;
        }
    }
}
