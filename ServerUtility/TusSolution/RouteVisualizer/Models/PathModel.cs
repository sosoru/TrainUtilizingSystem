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
        : Model, IEdge<GateModel, PathModel>, IEquatable<PathModel>
    {
        private PathData _baseData;

        public PathModel(PathData path)
        {
            this._baseData = path;

            this.PreviousGate = new GateModel(path.GateStart);
            this.NextGate = new GateModel(path.GateEnd);

            if (this._baseData.CurveCenter != null)
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
                    return Math.Abs(this._baseData.Angle.dtor() * this._baseData.Length);
                }
            }
        }

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

        #region implementation of IEqualable
        public static bool operator ==(PathModel A, PathModel B)
        {
            if (ReferenceEquals(A, B))
                return true;
            else if ((object)A == null || (object)B == null)
                return false;
            else
                return (A._baseData.ID== B._baseData.ID );
        }
        public static bool operator !=(PathModel A, PathModel B) { return !(A == B); }

        public bool Equals(PathModel other)
        {
            return (this == other);
        }

        public override int GetHashCode()
        {
            return this._baseData.ID.GetHashCode() ^ this._baseData.ID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this == (PathModel)obj;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
        #endregion


        public GateModel NextGate
        {
            get;
            private set;
        }

        public GateModel PreviousGate
        {
            get;
            private set;
        }
    }
}
