using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouteVisualizer.EF
{
    //should poco
    public class RailData
    {
        public RailData() { }

        public int ID { get; set; }

        public string Manifacturer { get; set; }
        public string RailName { get; set; }
        public virtual GateData BottomGate { get; set; }

        public virtual ICollection<GateData> Gates { get; set; }
        
        public virtual ICollection<PathData> Pathes {get;set;}
    }

    public class GateData
    {
        public GateData()
        { }

        public int ID { get; set; }

        public string GateName { get; set; }

        public int RailID { get;set;}
    }

    public class PathData
    {
        public PathData() { }

        public int ID { get; set; }

        public int RailID { get; set; }

        public GateData GateStart { get; set; }
        public GateData GateEnd { get; set; }

        public bool IsStraight { get; set; }

        public double StraightLength { get; set; }

        public double Radius { get; set; }
        public double Angle { get; set; }

    }

}
