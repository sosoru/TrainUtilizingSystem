using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouteVisualizer
{
    public static class Extensions
    {
        public static double dtor(this double degree)
        {
            return degree / 180.0 * Math.PI;
        }

        public static double rtod(this double radian)
        {
            return radian * Math.PI / 180.0;
        }
    }
}
