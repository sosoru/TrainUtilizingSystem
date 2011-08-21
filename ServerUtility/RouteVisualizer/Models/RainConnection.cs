using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouteVisualizer.Models
{
    public class RailConnection
        : IGate, IDrawable
    {
        public IEnumerable<IPath> ConnectedPathes
        {
            get { throw new NotImplementedException(); }
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
