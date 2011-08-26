using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace RouteVisualizer.Models
{
    public class PysicalPath
        : IPath, IDrawable
    {

        public double Length { get; set; }

        public IGate PreviousGate { get; set; }

        public IGate NextGate { get; set; }

        public bool ElectricalConnection { get; set; }

        public Drawing CurrentDrawing
        {
            get { throw new NotImplementedException(); }
        }

        public Rect Bound
        {
            get { throw new NotImplementedException(); }
        }

        public Rail OwnerRail { get; set; }
    }
}
