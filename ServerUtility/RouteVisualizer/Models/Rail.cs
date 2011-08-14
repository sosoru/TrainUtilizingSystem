using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouteVisualizer.Models
{
    public class Rail
        : IDrawable
    {
        public IList<PysicalPath> Pathes { get; private set; }
        public IList<RainConnection> Connections { get; private set; }

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
