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
using System.Windows.Media.Animation;

using SensorLivetView.ViewModels.Controls;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Threading;

namespace SensorLivetView.Views
{
    /// <summary>
    /// VoltageGraphView.xaml の相互作用ロジック
    /// </summary>
    public partial class VoltageGraphView : UserControl
    {
        List<Line> lines = new List<Line>();
        System.Windows.Threading.DispatcherTimer uitimer;

        DoubleAnimation lineXanim;
        DoubleAnimation lineYanim;
        AnimationClock lineXclock;
        AnimationClock lineYclock;

        VoltageGraphViewModel ViewModel
        {
            get
            {
                return this.DataContext as VoltageGraphViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        public VoltageGraphView()
        {
            InitializeComponent();

            this.uitimer = new DispatcherTimer();
            uitimer.Tick += new EventHandler(uitimer_Tick);

            int cap = 100;
            for (int i = 0; i < cap; ++i)
            {
                var line = new Line();
                line.Stroke = Brushes.Red;
                line.StrokeThickness = 1.0;
                lines.Add(line);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!uitimer.IsEnabled)
                this.uitimer.Start();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!this.IsLoaded)
                return;

            if (uitimer.IsEnabled)
                uitimer.Stop();

            var oldvm = e.OldValue as VoltageGraphViewModel;
            var newvm = e.NewValue as VoltageGraphViewModel;

            if (newvm != null)
            {
                uitimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                uitimer.Start();
            }
        }

        void uitimer_Tick(object sender, EventArgs e)
        {
            var vm = this.ViewModel;
            if (vm == null || vm.DataProvider == null)
                return;

            var val = vm.DataProvider.GetNext;

            RefreshLine(val);
        }

        private int refreshind = 0;
        private void RefreshLine(double y)
        {
            y = (y > 1.0) ? 1.0 : y;
            if (y == double.NaN)
                return;

            if (refreshind >= lines.Count)
            {
                refreshind = 0;
                cnv.Children.Clear();
            }


            var li = lines [refreshind];
            var befli = (refreshind - 1 < 0) ? lines.Last() : lines [refreshind - 1];
            var lenx = cnv.ActualWidth / ((double)lines.Count);
            var animateDuration = new Duration(new TimeSpan(0, 0, 0, 0, 500));

            li.X1 = lenx * refreshind;
            li.X2 = li.X1;
            var destX = lenx * (refreshind + 1);

            li.Y1 = befli.Y2; // valid if refrehed before line
            li.Y2 = li.Y1;
            var destY = cnv.ActualHeight * (1.0 - y);

            this.lineXanim = new DoubleAnimation(destX, animateDuration);
            li.ApplyAnimationClock(Line.X2Property, null);
            this.lineXclock = lineXanim.CreateClock();
            li.ApplyAnimationClock(Line.X2Property, this.lineXclock);

            this.lineYanim = new DoubleAnimation(destY, animateDuration);
            li.ApplyAnimationClock(Line.Y2Property, null);
            this.lineYclock = lineYanim.CreateClock();
            li.ApplyAnimationClock(Line.Y2Property, this.lineYclock);

            cnv.Children.Add(li);
            this.lineXclock.Controller.Begin();
            this.lineYclock.Controller.Begin();

            ++refreshind;
        }
    }
}