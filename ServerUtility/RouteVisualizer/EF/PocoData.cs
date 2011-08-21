using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouteVisualizer.EF
{
    //should poco
    public class RailData
    {
        public int ID { get; set; }

        public string Manifacturer { get; set; }
        public string RailName { get; set; }
        public string BottomGate { get; set; }

        public virtual ICollection<string> Gates { get; set; }
        
        public virtual ICollection<PathData> Pathes {get;set;}
    }

    public class PathData
    {
        public int ID { get; set; }

        public int RailID { get; set; }

        public string GateStart { get; set; }
        public string GateEnd { get; set; }

        public bool IsStraight { get; set; }

        public double StraightLength { get; set; }

        public double Radius { get; set; }
        public double Angle { get; set; }

    }

}
