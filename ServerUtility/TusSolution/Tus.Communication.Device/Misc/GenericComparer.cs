using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tus.Communication
{
    public class GenericComparer<T>
        : EqualityComparer<T>
    {
        private Func<T, T, bool> compFunc;

        public GenericComparer(Func<T,T,bool> compFunc)
            : base()
        {
            this.compFunc = compFunc;
        }

        public override bool Equals(T x, T y)
        {
            return this.compFunc(x, y);
        }

        public override int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
