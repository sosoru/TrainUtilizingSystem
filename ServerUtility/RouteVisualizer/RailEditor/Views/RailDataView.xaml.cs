using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;

using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

using RouteVisualizer.RailEditor.ViewModels;

namespace RouteVisualizer
{
	/// <summary>
	/// RailDataView.xaml の相互作用ロジック
	/// </summary>
	public partial class RailDataView : UserControl
	{
		public RailDataView()
		{
			this.InitializeComponent();
		}

        public GateStoringCollection GatesOnView { get { return this.Resources ["GateStoringCollectionDataSource"] as GateStoringCollection; } }
        
        private void gates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.Cast<GateDataViewModel>())
                    this.GatesOnView.Add(item);
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems.Cast<GateDataViewModel>())
                    this.GatesOnView.Remove(item);
            }
        }

		private void UserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
		{
            if (this.GatesOnView == null)
                return;

            this.GatesOnView.Clear();

            if (e.OldValue != null)
            {
                var dc = e.OldValue as RailDataViewModel;
                if (dc != null)
                {
                    dc.gates.CollectionChanged -= gates_CollectionChanged;
                }
            }

            if (e.NewValue != null)
            {
                var dc = e.NewValue as RailDataViewModel;
                if (dc != null)
                {
                    dc.gates.CollectionChanged += gates_CollectionChanged;
                }
            }
		}
	}

    public class GateStoringCollection : ObservableCollection<GateDataViewModel> { }
}