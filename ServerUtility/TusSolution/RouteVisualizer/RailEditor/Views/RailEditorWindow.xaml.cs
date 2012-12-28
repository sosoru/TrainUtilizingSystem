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
using System.Windows.Shapes;
using RouteVisualizer.RailEditor.ViewModels;

namespace RouteVisualizer.RailEditor.Views
{
    /// <summary>
    /// RailEditorWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class RailEditorWindow : Window
    {
        public RailEditorWindow()
        {
            this.InitializeComponent();

            // オブジェクト作成に必要なコードをこの下に挿入します。
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var casted = this.DataContext as RailEditorWindowViewModel;
            if (casted == null)
                return;

            var tr = sender as TreeView;
            var node = tr.SelectedItem as RailEditorWindowViewModel.RailEditorViewNode;

            if (node == null || node.ViewModel == null)
                return;

            casted.SelectedNode = node.ViewModel;

            if (casted.SaveCommand.CanExecute())
                casted.SaveCommand.Execute();

        }
    }
}