using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using RouteVisualizer;
using RouteVisualizer.EF;

namespace RouteVisualizer.Models
{
    public class PathModel
        : Model, IPath
    {
        private PathData _baseData;

        public PathModel(PathData path)
        {
            this._baseData = path;

            this.PreviousGate = new GateModel(path.GateStart);
            this.NextGate = new GateModel(path.GateEnd);

            this.CurveCenter = new Point(this._baseData.CurveCenter.First(), this._baseData.CurveCenter.Last());
        }

        public bool IsStraight
        {
            get { return this._baseData.IsStraight; }
        }

        public double Angle
        {
            get { return this._baseData.Angle; }
        }

        public double StartAngle
        {
            get { return this._baseData.StartAngle; }
        }

        public Point CurveCenter
        {
            get;
            private set;
        }

        public double Radius
        {
            get
            {
                if (this.IsStraight)
                    return this._baseData.Length / 2.0;
                else
                    return this._baseData.Length;
            }
        }

        public double Length
        {
            get
            {
                if (this._baseData.IsStraight)
                {
                    return this._baseData.Length;
                }
                else
                {
                    return Math.Abs( this._baseData.Angle.dtor() * this._baseData.Length);
                }
            }
        }

        public GateModel PreviousGate { get; private set; }
        public GateModel NextGate { get; private set; }

        bool _ElectricalConnection;

        public bool ElectricalConnection
        {
            get
            { return _ElectricalConnection; }
            set
            {
                if (_ElectricalConnection == value)
                    return;
                _ElectricalConnection = value;
                RaisePropertyChanged("ElectricalConnection");
            }
        }

        IGate IPath.PreviousGate
        {
            get { return ((PathModel)this).PreviousGate; }
        }

        IGate IPath.NextGate
        {
            get { return ((PathModel)this).NextGate; }
        }
    }
}
