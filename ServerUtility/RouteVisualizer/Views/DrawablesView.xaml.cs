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
using System.ComponentModel;
using System.Collections.ObjectModel;

using RouteVisualizer.ViewModels;
using RouteVisualizer.Models;

namespace RouteVisualizer
{
	/// <summary>
	/// DrawingCanvas.xaml の相互作用ロジック
	/// </summary>
	public partial class DrawablesView : UserControl
	{
        private ObservableWrappingCollection<IDrawable,Rectangle> rectList;

		public DrawablesView()
		{
			this.InitializeComponent();
		}

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            rectList = new ObservableWrappingCollection<IDrawable,Rectangle>();

        }

        private Rectangle rectList_Projection(IDrawable src)
        {
            var geo = src.CurrentGeometry;
            var bound = geo.Bounds;
            var vrect = new Rectangle();

            //var 

            //cnv.Children.Add(vrect);
            Canvas.SetLeft(vrect,bound.Left);
            Canvas.SetRight(vrect, bound.Right);
            Canvas.SetBottom(vrect, bound.Bottom);
            Canvas.SetTop(vrect, bound.Top);
            return null;
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = this.DataContext as DrawablesViewModel;

            if (vm != null)
            {
                this.rectList.Context = vm.Drawables;
            }
            else
            {
                this.rectList.Context = null;
            }

        }
	}
}