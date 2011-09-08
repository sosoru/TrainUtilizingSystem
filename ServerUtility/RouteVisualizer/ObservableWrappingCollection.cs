using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;

namespace RouteVisualizer
{
    public class ObservableWrappingCollection<Tsrc, Tproj>
        : ObservableCollection<Tproj>
        where Tsrc : class 
        where Tproj : class
    {
        private IList<Tsrc> _context;
        public IList<Tsrc> Context
        {
            get { return this._context; }
            set
            {
                this._context = value;
                this.Clear();
                foreach (var item in this._context)
                {
                    this.Items.Add(Projection(item));
                }
                // do not cause CollectionChanged to avoid to duplicate items of the wrapped collection.
            }
        }

        private Func<Tsrc, Tproj> _proj;
        public Func<Tsrc, Tproj> Projection 
        {
            get
            {
                return _proj ?? (_proj = (s) => s as Tproj);
            }
            set { _proj = value; }
        }

        private Func<Tproj, Tsrc> _invproj;
        public Func<Tproj, Tsrc> InverseProjection
        {
            get
            {
                return _invproj ?? (_invproj = (p) => p as Tsrc);
            }
            set { _invproj = value; }
        }

        protected override void InsertItem(int index, Tproj item)
        {
            base.InsertItem(index, item);
            this.Context.Add(InverseProjection(item));
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            this.Context.RemoveAt(index);
        }
    }
}
