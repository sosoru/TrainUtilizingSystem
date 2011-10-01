//#define TEST

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
using SensorLibrary;
using SensorLibrary.Manipulators;

namespace SensorLivetView.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel { get { return this.Resources ["MainWindowViewModelDataSource"] as MainWindowViewModel; } }

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel.AssociatedDispatcher = this.Dispatcher;
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

        private void pointmanipulate(PointModuleState state)
        {
            if (this.ViewModel == null)
                return;

            var man = new PointManipulator()
            {
                Target = this.ViewModel.AvailablePointModuleVMs.First().Model,
                To = state,
            };

            man.ExecuteFunc();
        }

        private void MiddleToOuter_Click(object sender, RoutedEventArgs e)
        {
            var state = new PointModuleState();

            state.SetPointState(0, PointStateEnum.Any); // A
            state.SetPointState(1, PointStateEnum.Any); // B
            state.SetPointState(2, PointStateEnum.Any); // C
            state.SetPointState(3, PointStateEnum.Any); // D
            state.SetPointState(4, PointStateEnum.Any); // E
            state.SetPointState(5, PointStateEnum.Any); // F
            state.SetPointState(6, PointStateEnum.Any); // G
            state.SetPointState(7, PointStateEnum.Any); // H

            this.pointmanipulate(state);
        }

        private void OuterToMiddle_Click(object sender, RoutedEventArgs e)
        {
            var state = new PointModuleState();

            state.SetPointState(0, PointStateEnum.Any); // A
            state.SetPointState(1, PointStateEnum.Any); // B
            state.SetPointState(2, PointStateEnum.Any); // C
            state.SetPointState(3, PointStateEnum.Any); // D
            state.SetPointState(4, PointStateEnum.Any); // E
            state.SetPointState(5, PointStateEnum.Any); // F
            state.SetPointState(6, PointStateEnum.Any); // G
            state.SetPointState(7, PointStateEnum.Any); // H

            this.pointmanipulate(state);

        }

        private void MiddleToInner_Click(object sender, RoutedEventArgs e)
        {
            var state = new PointModuleState();

            state.SetPointState(0, PointStateEnum.Any); // A
            state.SetPointState(1, PointStateEnum.Any); // B
            state.SetPointState(2, PointStateEnum.Any); // C
            state.SetPointState(3, PointStateEnum.Any); // D
            state.SetPointState(4, PointStateEnum.Any); // E
            state.SetPointState(5, PointStateEnum.Any); // F
            state.SetPointState(6, PointStateEnum.Any); // G
            state.SetPointState(7, PointStateEnum.Any); // H

            this.pointmanipulate(state);

        }

        private void InnerToMiddle_Click(object sender, RoutedEventArgs e)
        {
            var state = new PointModuleState();

            state.SetPointState(0, PointStateEnum.Any); // A
            state.SetPointState(1, PointStateEnum.Any); // B
            state.SetPointState(2, PointStateEnum.Any); // C
            state.SetPointState(3, PointStateEnum.Any); // D
            state.SetPointState(4, PointStateEnum.Any); // E
            state.SetPointState(5, PointStateEnum.Any); // F
            state.SetPointState(6, PointStateEnum.Any); // G
            state.SetPointState(7, PointStateEnum.Any); // H

            this.pointmanipulate(state);

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
