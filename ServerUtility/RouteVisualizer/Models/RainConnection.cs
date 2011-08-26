using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                this._connectedPathes.Clear();
            }
        }

        private IList<IPath> _connectedPathes;
        public IEnumerable<IPath> ConnectedPathes
        {
            get { return this._connectedPathes; }
        }

        public System.Windows.Media.Drawing CurrentDrawing
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Rect Bound
        {
            get { throw new NotImplementedException(); }
        }
    }
}
