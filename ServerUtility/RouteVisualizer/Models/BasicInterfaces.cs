using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace RouteVisualizer.Models
{
    public interface IPath
    {
        double Length { get; }
        IGate PreviousGate { get; }
        IGate NextGate { get; }
    }

    public interface IGate
    {
        IEnumerable<IPath> ConnectedPathes { get; }
    }

    public interface IDrawable
    {
        Drawing CurrentDrawing { get; }
        Rect Bound { get; }
    }

}
