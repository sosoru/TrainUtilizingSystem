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
    public class RailConnection
        : IGate, IDrawable
    {
        public RailConnection()
        {
            this._connectedPathes = new List<IPath>();
        }

        private GateData _baseData;
        public GateData BaseData
        {
            get { return this._baseData; }
            set
            {
                this._baseData = value;

                this._connectedPathes.Clear();
            }
        }

        private IList<IPath> _connectedPathes;
        public IList<IPath> ConnectedPathes
        {
            get { return this._connectedPathes; }
        }

        public Geometry CurrentGeometry
        {
            get
            {
                var geo = new EllipseGeometry(new Rect(this.BasePosition, new Size(35.0, 35.0)));

                return geo;
            }
        }

        public Rect Bound
        {
            get
            {
                return new Rect(this.BasePosition, new Size(35.0, 35.0));
            }
        }

        public Point BasePosition { get; set; }

        public override string ToString()
        {
            return string.Format("Name : {0}", this.BaseData.GateName);
        }
    }
}
