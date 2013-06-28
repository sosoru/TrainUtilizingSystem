using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace RailroaderIO
{
    public class RailroaderRailData
    {
        public RailroaderRailData(IList<string> data)
        {
            if (data.Count < 61)
                throw new InvalidOperationException("insufficient list count");

            this.internalData_ = new ReadOnlyCollection<string>(data);

            this.IsVisible = railroaderBoolParse(data [0]);
            this.id = int.Parse(data [1]);
            this.name = data [2];
            this.sort = (Sort)Enum.Parse(typeof(Sort), data [3]);
            //data[4];
            this.StraightLength = float.Parse(data [5]);
            this.CurveLength = float.Parse(data [6]);
            this.CurveDegree = int.Parse(data [7]);
            //data[8];
            this.CornerPos1X = float.Parse(data [9]);
            this.CornerPos1Y = float.Parse(data [10]);
            this.CornerPos2X = float.Parse(data [11]);
            this.CornerPos2Y = float.Parse(data [12]);
            this.CornerPos3X = float.Parse(data [13]);
            this.CornerPos3Y = float.Parse(data [14]);
            this.UpperLeft_x = float.Parse(data [15]);
            this.UpperLeft_y = float.Parse(data [16]);
            this.BottomLeft_x = float.Parse(data [17]);
            this.BottomLeft_y = float.Parse(data [18]);
            this.BottomRight_x = float.Parse(data [19]);
            this.BottomRight_y = float.Parse(data [20]);
            this.UpperRight_x = float.Parse(data [21]);
            this.UpperRight_y = float.Parse(data [22]);
            this.BottomRight2_x = float.Parse(data [23]);
            this.BottomRight2_y = float.Parse(data [24]);
            this.UpperRight2_x = float.Parse(data [25]);
            this.UpperRight2_y = float.Parse(data [26]);
            this.CurveCenterPos_x = float.Parse(data [27]);
            this.CurveCenterPos_y = float.Parse(data [28]);
            this.CenterPos_x = float.Parse(data [29]);
            this.CenterPos_y = float.Parse(data [30]);
            //data[31];
            //data[32];
            this.StartGradient = int.Parse(data [33]);
            this.EndGradient = int.Parse(data [34]);
            this.ThirdGradient = int.Parse(data [35]);
            //data[36];
            this.CurveRadian = float.Parse(data [37]);
            //data[38];
            //data[39];
            //data[40];
            //data[41];
            //data[42];
            //data[43];
            //data[44];
            //data[45];
            //data[46];
            //data[47];
            //data[48];
            this.NeighborRail1Cnt = int.Parse(data [49]);
            this.NeighborRail1 = int.Parse(data [50]);
            this.NeighborRail2Cnt = int.Parse(data [51]);
            this.NeighborRail2 = int.Parse(data [52]);
            this.NeighborRail3Cnt = int.Parse(data [53]);
            this.NeighborRail3 = int.Parse(data [54]);
            this.IsElevated = railroaderBoolParse(data [55]);
            //data[56];
            //data[57];
            //data[58];
            this.Color = int.Parse(data [59]);
            this.InternalId = int.Parse(data [60]);

        }

        private bool railroaderBoolParse(string str)
        {
            return str.Contains("TRUE");
        }

        private ReadOnlyCollection<string> internalData_;
        public ReadOnlyCollection<string> InternalData
        {
            get
            {
                return internalData_;
            }
        }

        public bool IsVisible { get; set; }

        public int id { get; set; }
        public string name { get; set; }
        public Sort sort { get; set; }

        public float CurveLength { get; set; }
        public float StraightLength { get; set; }
        public int CurveDegree { get; set; }

        public float CornerPos1X { get; set; }
        public float CornerPos1Y { get; set; }
        public float CornerPos2X { get; set; }
        public float CornerPos2Y { get; set; }
        public float CornerPos3X { get; set; }
        public float CornerPos3Y { get; set; }

        public float UpperLeft_x { get; set; }
        public float UpperLeft_y { get; set; }
        public float BottomLeft_x { get; set; }
        public float BottomLeft_y { get; set; }
        public float BottomRight_x { get; set; }
        public float BottomRight_y { get; set; }
        public float UpperRight_x { get; set; }
        public float UpperRight_y { get; set; }

        public float BottomRight2_x { get; set; }
        public float BottomRight2_y { get; set; }
        public float UpperRight2_x { get; set; }
        public float UpperRight2_y { get; set; }

        public float CurveCenterPos_x { get; set; }
        public float CurveCenterPos_y { get; set; }

        public float CenterPos_x { get; set; }
        public float CenterPos_y { get; set; }

        public int StartGradient { get; set; }
        public int EndGradient { get; set; }
        public int ThirdGradient { get; set; }

        public float CurveRadian { get; set; }

        public int NeighborRail1Cnt { get; set; }
        public int NeighborRail1 { get; set; }
        public int NeighborRail2Cnt { get; set; }
        public int NeighborRail2 { get; set; }
        public int NeighborRail3Cnt { get; set; }
        public int NeighborRail3 { get; set; }

        public bool IsElevated { get; set; }
        public int Color { get; set; }
        public int InternalId { get; set; }
    }

    public enum Sort : int
    {
        Straight = 0,
        Curve = 1,
        Point = 2,

        CombinatedRailMasterStraight = 50,
        CombinatedRailMasterCurve = 51,
        CombinatedRailSlaveStraight = 60,
        CombinatedRailSlaveCurved = 61,
    }
}
