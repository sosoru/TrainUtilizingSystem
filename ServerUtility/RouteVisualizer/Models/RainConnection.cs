using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouteVisualizer.Models
{
    public class RainConnection
        : IGate
    {
        public IEnumerable<IPath> ConnectedPathes
        {
            get { throw new NotImplementedException(); }
        }
    }
}
