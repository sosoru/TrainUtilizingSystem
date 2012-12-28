using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

using RouteVisualizer;
using RouteVisualizer.EF;
using RouteVisualizer.Models;
using RailroaderIO;
using System.IO;

namespace RouteVisualizer.Railroader
{
    public static class RailroaderExtensions
    {
        public static LayoutModel ToLayout(this RailroaderMap map)
        {
            var layout = new LayoutModel();

            var connections = new SortedDictionary<int, GateData>();

            foreach (var rail in map.Rails)
            {
                if (!rail.IsVisible)
                    continue;

                var raildata = new RailData()
                {
                    ID = rail.id,
                    RailName = rail.name,
                    Gates = new List<GateData>(),
                    Pathes = new List<PathData>(),
                };

                //set gates
                var firstgate = new GateData()
                {
                    ID = (rail.id << 16) + 1,
                    RailID = rail.id,
                    GateName = string.Format("id:{0} first on railroader", rail.id),
                    Position = new [] { (double)rail.CornerPos1X, (double)rail.CornerPos1Y },
                };
                var secondgate = new GateData()
                {
                    ID = (rail.id << 16) + 2,
                    RailID = rail.id,
                    GateName = string.Format("id:{0} second on railroader", rail.id),
                    Position = new [] { (double)rail.CornerPos2X, (double)rail.CornerPos2Y },
                };
                raildata.Gates.Add(firstgate);
                raildata.Gates.Add(secondgate);

                GateData thirdgate = null;
                if (rail.sort == Sort.Point)
                {
                    thirdgate = new GateData()
                    {
                        ID = (rail.id << 16) + 3,
                        RailID = rail.id,
                        GateName = string.Format("id:{0} third on railroader", rail.id),
                        Position = new [] { (double)rail.CornerPos3X, (double)rail.CornerPos3Y },
                    };
                    raildata.Gates.Add(thirdgate);
                }

                //set pathes
                PathData straightpath= null, curvepath= null;
                straightpath = new PathData()
                {
                    ID = (rail.id << 16) + 1,
                    RailID = rail.id,
                    IsStraight = true,
                    Length = rail.StraightLength,
                };

                if (rail.CurveLength > 0.0f)
                {
                    var xL = rail.CornerPos1X - rail.CurveCenterPos_x;
                    var yL = rail.CornerPos1Y - rail.CurveCenterPos_y;
                    var r = Math.Sqrt(xL * xL + yL * yL);

                    curvepath = new PathData()
                   {
                       ID = (rail.id << 16) + 2,
                       RailID = rail.id,
                       IsStraight = false,
                       Length = rail.CurveLength,

                       CurveCenter = new [] { (double)rail.CurveCenterPos_x, (double)rail.CurveCenterPos_y },
                   };
                }

                switch (rail.sort)
                {
                    case Sort.Straight:
                    case Sort.CombinatedRailMasterStraight:
                    case Sort.CombinatedRailSlaveStraight:
                        straightpath.GateStart = firstgate;
                        straightpath.GateEnd = secondgate;

                        straightpath.StartAngle = (rail.StartGradient - 90) % 360;
                        straightpath.EndAngle = (rail.EndGradient + 90) % 360;

                        raildata.Pathes.Add(straightpath);
                        break;
                    case Sort.CombinatedRailMasterCurve:
                    case Sort.CombinatedRailSlaveCurved:
                    case Sort.Curve:
                        curvepath.GateStart = firstgate;
                        curvepath.GateEnd = secondgate;

                        curvepath.StartAngle = (rail.StartGradient - 90) % 360;
                        curvepath.EndAngle = (rail.EndGradient + 90) % 360;

                        raildata.Pathes.Add(curvepath);
                        break;
                    case Sort.Point: //neighbor2が常にstraight
                        straightpath.GateStart = firstgate;
                        straightpath.GateEnd = secondgate;
                        curvepath.GateStart = firstgate;
                        curvepath.GateEnd = thirdgate;

                        straightpath.StartAngle = (rail.StartGradient - 90) % 360;
                        straightpath.EndAngle = (rail.EndGradient + 90) % 360;
                        curvepath.StartAngle = (rail.StartGradient - 90) % 360;
                        curvepath.EndAngle = (rail.ThirdGradient + 90) % 360;

                        raildata.Pathes.Add(straightpath);
                        raildata.Pathes.Add(curvepath);
                        break;
                }

                foreach (var neighbor in new [] {new {neighbor=rail.NeighborRail1, gate = firstgate},
                                                new {neighbor=rail.NeighborRail2, gate = secondgate},
                                                new {neighbor=rail.NeighborRail3, gate = thirdgate},})
                {
                    if (neighbor.neighbor != 0)
                    {
                        var i = 0;

                        for(;;)
                        {
                            var id = (neighbor.neighbor << 16) + i;
                            var val = neighbor.gate;

                            if (connections.ContainsKey(id))
                            {
                                i++;
                                continue;
                            }
                            else
                            {
                                connections.Add(id, val);
                                break;
                            }
                        }

                    }
                }

                var railmodel = new RailModel(raildata);
                layout.Rails.Add(railmodel);
            }

            connections.GroupBy(pair => pair.Key >> 16)
                       .Select(g => new GateConnectionData()
                               {
                                   ID = g.Key,
                                   ConnectedGates = g.Select(pair => pair.Value).ToList(),
                               })
                       .Select(data => new GateConnectionModel(data))
                       .ToList()
                       .ForEach(layout.PhysicalConnections.Add);

            layout.LayoutSize = new Size(map.LayoutWidth, map.LayoutHeight);

            return layout;
        }
    }
}
