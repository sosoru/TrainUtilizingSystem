using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace RouteVisualizer.EF
{
    public class ModelingDatabase
        : DbContext
    {
        public ModelingDatabase()
            : base()
        {
        }
        public ModelingDatabase(string connStr)
            : base(connStr)
        {
        }

        public IDbSet<RailData> Rails { get; set; }
        public IDbSet<PathData> Pathes { get; set; }
    }
}
