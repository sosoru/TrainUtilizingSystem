using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using RouteVisualizer;
using RouteVisualizer.EF;
using System.Reactive.Linq;

namespace RouteVisualizer.Models
{
    public class Rail
        : IDrawable
    {
        public Rail()
        {
        }

        private RailData _baseData;
        public RailData BaseData
        {
            get
            {
                return this._baseData;
            }
            set
            {
                if (this.Pathes != null)
                    this.Pathes.Clear();

                if (this.Connections != null)
                    this.Connections.Clear();

                this._baseData = value;

                if (this._baseData != null)
                {
                    this.Connections = this._baseData.Gates.Select((g) => new RailConnection()
                                        {
                                            BaseData = g,
                                        }).ToList();

                    this.Pathes = this._baseData.Pathes.Select((p) => new PysicalPath()
                                        {
                                            BaseData = p,
                                            OwnerRail = this,
                                            PreviousGate = this.Connections.First((conn) => conn.BaseData.ID == p.GateStart.ID),
                                            NextGate = this.Connections.First(conn => conn.BaseData.ID == p.GateEnd.ID),
                                        }).ToList();

                }
            }
        }

        public ICollection<PysicalPath> Pathes { get; private set; }

        public ICollection<RailConnection> Connections { get; private set; }

        public double Rotation { get; set; }
        public bool IsMirrored { get; set; }
        public virtual RailConnection BaseConnection { get; set; }

        public bool IsPathValidated
        {
            get
            {
                try
                {
                    LocateGate();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        public IDictionary<IGate, Point> LocateGate()
        {
            if (this.Pathes == null || this.Pathes.Count == 0)
                return new Dictionary<IGate, Point>();

            if (this.BaseConnection == null)
                this.BaseConnection = this.Connections.First();

            var dict = new Dictionary<IGate, Point>();
            foreach (var conn in this.Connections)
                dict.Add(conn, new Point());

            //check isolated gate
            if (this.Connections.Any((conn) => conn.ConnectedPathes.Count == 0))
                throw new InvalidOperationException("isolated gate found");

            foreach (var path in this.Pathes)
            {
                Point sentpoint = path.Bound.TopRight;
                var basepoint = dict [path.PreviousGate];

                sentpoint.Offset(basepoint.X, basepoint.Y);

                //check overwrite
                var zero = new Point();
                if (dict [path.NextGate] != zero && dict [path.NextGate] != sentpoint)
                {
                    throw new InvalidOperationException(string.Format("gate position mismatching : {0}", path.NextGate.ToString()));
                }

                dict [path.NextGate] = sentpoint;
            }

            return dict;
        }

        public virtual Geometry CurrentGeometry
        {
            get
            {
                var geogroup = new GeometryGroup();
                var gateposes = this.LocateGate();

                foreach (var conn in this.Connections)
                {
                    conn.BasePosition = gateposes [conn];

                    var geo  = conn.CurrentGeometry;
                    geogroup.Children.Add(geo);
                }

                foreach (var path in this.Pathes)
                {
                    var geo  =path.CurrentGeometry;
                    geogroup.Children.Add(geo);
                }

                return geogroup;
            }
        }

        public virtual Rect Bound
        {
            get
            {
                var rect = new Rect();
                foreach (var p in this.Pathes)
                    rect.Union(p.Bound);
                foreach (var conn in this.Connections)
                    rect.Union(conn.Bound);

                return rect;
            }
        }
    }

}
