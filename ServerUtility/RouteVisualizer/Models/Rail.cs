using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace RouteVisualizer.Models
{
    public abstract class Rail
        : IDrawable
    {
        public Rail()
        {
        }

        public virtual ICollection<PysicalPath> Pathes { get; private set; }
        public virtual ICollection<RailConnection> Connections { get; private set; }

        public double Rotation { get; set; }
        public virtual RailConnection BaseConnection { get; set; }

        public virtual Drawing CurrentDrawing
        {
            get { throw new NotImplementedException(); }
        }

        public virtual Rect Bound
        {
            get { throw new NotImplementedException(); }
        }
    }

}
