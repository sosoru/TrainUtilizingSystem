using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
//using System.Data.Entity;

namespace SensorViewLibrary
{
    public class ObservableWrappingCollection<Tsrc, Tproj>
        : ObservableCollection<Tproj>
        where Tsrc : class
        where Tproj : class
    {
        private NotifyCollectionChangedEventHandler published = null;

        private ObservableCollection<Tsrc> _context;
        public ObservableCollection<Tsrc> Context
        {
            get { return this._context; }
            set
            {
                if (this._context != null)
                    this._context.CollectionChanged -= published;

                this._context = value;
                if (this._context != null)
                {
                    this.Clear();
                    foreach (var item in this._context)
                    {
                        this.Items.Add(Projection(item));
                    }

                    this.published = new NotifyCollectionChangedEventHandler(value_CollectionChanged);
                    this._context.CollectionChanged += published;

                }
            }
        }

        private bool calledbySource_insert = false;
        private bool calledbySource_remove = false;
        void value_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                calledbySource_insert = true;
                this.InsertItem(e.NewStartingIndex, Projection(e.NewItems [0] as Tsrc));

                calledbySource_insert = false;
            }

            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                calledbySource_remove = true;
                this.RemoveItem(e.OldStartingIndex);

                calledbySource_remove = false;
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
            if (calledbySource_insert) // avoid to call recursively
                return;

            this.Context.Add(InverseProjection(item));
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            if (calledbySource_remove)
                return;

            this.Context.RemoveAt(index);
        }

    }
}
