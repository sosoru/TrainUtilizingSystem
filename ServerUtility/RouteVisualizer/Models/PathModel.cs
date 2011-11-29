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
        : IPath, IDrawable
    {
        private PathData _cache_baseData;
        public PathData BaseData
        {
            get
            {
                return this._cache_baseData;
            }
            set
            {
                this._cache_baseData = value;

            }
        }

        public double Length
        {
            get
            {
                if (this.BaseData == null)
                    return double.NaN;

                if (this.BaseData.IsStraight)
                {
                    return this.BaseData.Length;
                }
                else
                {
                    return this.BaseData.Angle.dtor() * this.BaseData.ViewRadius;
                }
            }
        }

        private IGate _previousGate;
        public IGate PreviousGate
        {
            get
            {
                return this._previousGate;
            }
            set
            {
                if (this._previousGate != null)
                {
                    this._previousGate.ConnectedPathes.Remove(this);
                }

                if (value != null)
                {
                    value.ConnectedPathes.Add(this);
                }

                this._previousGate = value;
            }
        }

        private IGate _nextGate;
        public IGate NextGate
        {
            get
            {
                return this._nextGate;
            }
            set
            {
                if (this._nextGate != null)
                {
                    this._nextGate.ConnectedPathes.Remove(this);
                }

                if (value != null)
                {
                    value.ConnectedPathes.Add(this);
                }

                this._nextGate = value;
            }

        }

        public bool ElectricalConnection { get; set; }

        public Geometry CurrentGeometry
        {
            get
            {
                if (this.BaseData == null)
                    return null;

                Geometry geo = null;
                var castedprev = this.PreviousGate as GateModel;
                var castednext = this.NextGate as GateModel;

                if (castedprev == null || castednext == null)
                    throw new InvalidOperationException("undrawable connection");

                if (this.BaseData.IsStraight)
                {
                    //todo : calc
                    var startpos = castedprev.BasePosition;
                    var endpos = castednext.BasePosition;
                    //endpos.Offset(this.BaseData.Length, 0.0);
                    geo = new LineGeometry(startpos, endpos);

                }
                else
                {
                    var r = this.BaseData.ViewRadius;
                    var t =  this.BaseData.Angle.dtor();
                    var startpos = castedprev.BasePosition;
                    var endpos = castednext.BasePosition;

                    //a = sqrt(2) * b * sqrt( 1- cos(t))

                    var centervec = new Point(this.BaseData.CurveCenter.First(), this.BaseData.CurveCenter.Last()) - startpos;
                    centervec.Normalize();
                    var tovec = endpos - startpos;
                    tovec.Normalize();

                    var clockwise = centervec.Y * tovec.X - centervec.X * tovec.Y >= 0.0;

                    var segment = new ArcSegment(endpos,
                                                  new Size(r, r),
                                                  0,
                                                  false,
                                                  (clockwise) ? SweepDirection.Clockwise : SweepDirection.Counterclockwise,
                                                  true);
                    geo = new PathGeometry(new [] { new PathFigure(startpos, new [] { segment }, false) });

                }

                return geo;
            }
        }

        public Rect Bound
        {
            get
            {
                if (this.BaseData == null)
                    return new Rect();

                var castedgate = this.PreviousGate as GateModel;
                if (castedgate == null)
                    throw new InvalidOperationException("undrawable connection");

                else
                {
                    if (this.BaseData.IsStraight)
                    {
                        return new Rect(castedgate.BasePosition, new Size(this.BaseData.Length, 0.0));
                    }
                    else
                    {
                        var r = this.BaseData.ViewRadius;
                        var sta = this.BaseData.StartAngle.dtor();
                        var end = this.BaseData.EndAngle.dtor();

                        var vecx = r * (Math.Sin(end) - Math.Sin(sta));
                        var vecy = r * (Math.Cos(end) - Math.Cos(sta));

                         //todo:impl calc
                       return new Rect(castedgate.BasePosition, (this.NextGate as GateModel).BasePosition);
                    }
                }

            }
        }

        public RailModel OwnerRail { get; set; }
    }
}
