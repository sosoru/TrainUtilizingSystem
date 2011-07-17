using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorLibrary
{
    public class WeakReference<T>
    {
        private WeakReference weak;

        public WeakReference(T obj)
        {
            this.weak = new WeakReference(obj);
        }

        public T Target
        {
            get
            {
                return (T)this.weak.Target;
            }
        }

        public bool IsAlive
        {
            get
            {
                return this.weak.IsAlive;
            }
        }

    }
}
