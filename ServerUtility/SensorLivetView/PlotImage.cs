using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SensorLibrary;
using SensorLibrary.Devices;
using Livet;
using Livet.Command;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;

namespace SensorLivetView
{
    public interface IGraphPainter
    {
        //void DrawPoints(Graphics g);
        double GetNext { get;}
    }

    public class GraphPainter<TDev, TState>
        : IGraphPainter
        where TState : IDeviceState<IPacketDeviceData>
        where TDev : IDevice<TState>
    {
        public TDev Device { get; set; }

        public Func<TState, double> DeterminateFunc { get; set; }
        public double GetNext
        {
            get
            {
                if (this.DeterminateFunc == null)
                    return double.NaN;

                return this.DeterminateFunc(this.Device.CurrentState);
            }
        }
    }
}
