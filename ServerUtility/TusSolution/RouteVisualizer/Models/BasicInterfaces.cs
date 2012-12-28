using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace RouteVisualizer.Models
{
    public interface IEdge<out Tgate, out Tedge>
        where Tgate : IGate<Tgate, Tedge>
        where Tedge : IEdge<Tgate, Tedge>
    {
        Tgate PreviousGate { get; }
        Tgate NextGate { get; }
    }

    public interface IGate<out Tgate, out Tedge>
        : IEnumerable<Tedge>
        where Tgate : IGate<Tgate, Tedge>
        where Tedge : IEdge<Tgate, Tedge>
    {
    }

    public interface IDrawable
    {
        Geometry CurrentGeometry { get; }
        Rect Bound { get; }
    }

}
