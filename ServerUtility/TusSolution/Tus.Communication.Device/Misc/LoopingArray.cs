using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Tus.Communication
{
    public class LoopingArray<T>
        : IEnumerable<T>
    {
        private int _curint = 0;
        private T[] _arr ;

        public LoopingArray(int count)
        {
            if (count <= 0)
                throw new ArgumentException("count <= 0");

            _arr = new T[count];
        }

        public void Push(T obj)
        {
            if (_curint >= _arr.Length)
                _curint = 0;
            this._arr[_curint++] = obj;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this._arr).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._arr.GetEnumerator();
        }
    }
}
