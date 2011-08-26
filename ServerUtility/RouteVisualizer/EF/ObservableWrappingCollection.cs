using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;

namespace RouteVisualizer.EF
{
    public class ObservableWrappingCollection<TSet, T>
        : ObservableCollection<T>
        where TSet : IDbSet<T>
        where T : class 
    {
        private TSet _context;
        public TSet Context
        {
            get { return this._context; }
            set
            {
                this._context = value;
                this.Clear();
                foreach (var item in this._context)
                {
                    this.Items.Add(item);
                }
                // do not cause CollectionChanged to avoid to duplicate items of the wrapped collection.
            }
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            this.Context.Add(item);
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            this.Context.Remove(base [index]);
        }
    }
}
