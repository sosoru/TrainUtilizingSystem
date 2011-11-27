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
    public class PysicalPath
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
                    return this.BaseData.StraightLength;
                }
                else
                {
                    return this.BaseData.Angle.dtor() * this.BaseData.Radius;
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
                var castedprev = this.PreviousGate as RailGate;
                var castednext = this.NextGate as RailGate;

                if (castedprev == null || castednext == null)
                    throw new InvalidOperationException("undrawable connection");

                if (this.BaseData.IsStraight)
                {

                    var startpos = castedprev.BasePosition;
                    var endpos = startpos;
                    endpos.Offset(this.BaseData.StraightLength, 0.0);
                    geo = new LineGeometry(startpos, endpos);

                }
                else
                {
                    var r = this.BaseData.Radius;
                    var t = this.BaseData.Angle.dtor();
                    var startpos = castedprev.BasePosition;
                    var endpos = new Point(r * Math.Sin(t), r * (1.0 - Math.Cos(t)));
                    endpos.Offset(startpos.X, startpos.Y);

                    //a = sqrt(2) * b * sqrt( 1- cos(t))
                    var segment = new ArcSegment(endpos,
                                                  new Size(r, r),
                                                  t,
                                                  false,
                                                  SweepDirection.Clockwise,
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

                var castedgate = this.PreviousGate as RailGate;
                if (castedgate == null)
                    throw new InvalidOperationException("undrawable connection");

                else
                {
                    if (this.BaseData.IsStraight)
                    {
                        return new Rect(castedgate.BasePosition, new Size(this.BaseData.StraightLength, 0.0));
                    }
                    else
                    {
                        var r = this.BaseData.Radius;
                        var t = this.BaseData.Angle.dtor();
                        return new Rect(castedgate.BasePosition, new Size(r * Math.Sin(t), r * (1.0 - Math.Cos(t))));
                    }
                }

            }
        }

        public Rail OwnerRail { get; set; }
    }
}
