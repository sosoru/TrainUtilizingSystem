#define TEST

using System;
using System.Collections.Generic;
using System.Linq;
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

using SensorLivetView.ViewModels;
using SensorLivetView.ViewModels.Controls;

namespace SensorLivetView.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel { get { return this.Resources["MainWindowViewModelDataSource"] as MainWindowViewModel; } }

        public MainWindow()
        {
            InitializeComponent();

            this.ViewModel.VenderID = 0x04D8;
            this.ViewModel.ProductID = 0x0204;
            this.ViewModel.InitCandicateDevice();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
#if TEST
            try
            {
                var testwnd = new TestWindow();
                testwnd.Show();
            }
            catch (Exception)
            {
                Console.WriteLine("");
            }
#endif
        }

        //private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (this.ViewModel != null)
        //    {
        //        if (this.ViewModel.RefreshDispatchersCommand.CanExecute())
        //        {
        //            this.ViewModel.RefreshDispatchersCommand.Execute();
                    
        //        }
        //    }
        //}
    }
}
