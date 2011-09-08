using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace SensorLibrary
{
    public interface IGraphPainter
    {
        //void DrawPoints(Graphics g);
        IList<PointF> GetGraphPointCollection(RectangleF canvas);
    }

    public class GraphPainter<TState>
        : IGraphPainter
        where TState : IDeviceState<IPacketDeviceData>
    {
        public IList<TState> States { get; set; }

        public Func<TState, RectangleF, float> plotXDetermine { get; set; }
        public Func<TState, RectangleF, float> plotYDetermine { get; set; }

        //public void DrawPoints(Graphics g)
        //{
        //    var list = GetGraphPointCollection(g.ClipBounds).ToList();

        //    for (int i = 0; i < list.Count() - 1; i++)
        //    {
        //        var pen = Pens.Black;

        //        var ptA = list[i];
        //        var ptB = list[i + 1];

        //        g.DrawLine(pen, ptA, ptB);
        //    }
        //}

        private class pointcmp
            : IComparer<PointF>
        {
            public int Compare(PointF x, PointF y)
            {
                return x.X.CompareTo(y.X);
            }
        }

        public IList<PointF> GetGraphPointCollection(RectangleF canvasRect)
        {
            var list = new SortedSet<PointF>(new pointcmp());

            foreach (var state in this.States)
            {
                var ptA = new PointF(plotXDetermine(state, canvasRect), plotYDetermine(state, canvasRect));

                list.Add(ptA);

            }
            return list.ToList();

        }

    }
}
