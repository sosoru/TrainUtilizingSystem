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
    public class GateModel
        : IGate, IDrawable
    {
        public GateModel(GateData data)
        {
            this.BaseData = data;

            this._connectedPathes = new List<IPath>();
        }

        public GateData BaseData { get; private set;}

        private IList<IPath> _connectedPathes;
        public IList<IPath> ConnectedPathes
        {
            get { return this._connectedPathes; }
        }

        public Geometry CurrentGeometry
        {
            get
            {
                var geo = new EllipseGeometry(this.Bound);

                return geo;
            }
        }

        public Rect Bound
        {
            get
            {
                return new Rect(this.BasePosition - new Vector(2.5, 2.5), new Size(5.0, 5.0));
            }
        }

        private Point? _basePosition = null;
        public Point BasePosition
        {
            get { return (_basePosition ?? (_basePosition = new Point(this.BaseData.Position.First(), this.BaseData.Position.Last()))).Value;}
            set { this._basePosition = value; }
        }

        public override string ToString()
        {
            return string.Format("Name : {0}", this.BaseData.GateName);
        }
    }
}
