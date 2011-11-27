using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                };
                var secondgate = new GateData()
                {
                    ID = (rail.id << 16) + 2,
                    RailID = rail.id,
                    GateName = string.Format("id:{0} second on railroader", rail.id),
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
                    StraightLength = rail.StraightLength
                };

                var angle = ((double)rail.CurveDegree / 180.0) * Math.PI;
                if (angle != 0.0)
                {
                    curvepath = new PathData()
                   {
                       ID = (rail.id << 16) + 2,
                       RailID = rail.id,
                       IsStraight = false,
                       Radius = (double)rail.CurveLength / angle,
                       Angle = angle,
                   };
                }
                
                switch (rail.sort)
                {
                    case Sort.Straight:
                    case Sort.CombinatedRailMaster:
                    case Sort.CombinatedRailSlave:
                        straightpath.GateStart = firstgate;
                        straightpath.GateEnd = secondgate;
                        raildata.Pathes.Add(straightpath);
                        break;
                    case Sort.Curve:
                        curvepath.GateStart = firstgate;
                        curvepath.GateEnd = secondgate;
                        raildata.Pathes.Add(curvepath);
                        break;
                    case Sort.Point : //neighbor2が常にstraight
                        straightpath.GateStart = firstgate;
                        straightpath.GateEnd = thirdgate;
                        curvepath.GateStart = firstgate;
                        curvepath.GateEnd = secondgate;
                        
                        raildata.Pathes.Add(straightpath);
                        raildata.Pathes.Add(curvepath);
                        break;
                }

                foreach (var neighbor in new [] {new {neighbor=rail.NeighborRail1, cnt = rail.NeighborRail1Cnt, gate = firstgate},
                                                new {neighbor=rail.NeighborRail2, cnt = rail.NeighborRail2Cnt, gate = secondgate},
                                                new {neighbor=rail.NeighborRail3, cnt= rail.NeighborRail3Cnt, gate = thirdgate},})
                {
                    if (neighbor.neighbor != 0)
                    {
                        connections.Add((neighbor.neighbor << 16) + neighbor.cnt, neighbor.gate);
                    }
                }

                var railmodel = new Rail(raildata);
                railmodel.LocateGate();
                layout.Rails.Add(railmodel);
            }

            connections.GroupBy(pair => pair.Key >> 16)
                       .Select(g => new GateConnectionData()
                               {
                                   ID = g.Key,
                                   ConnectedGates = g.Select(pair => pair.Value).ToList(),
                               })
                       .Select(data => new GateConnectionModel() { BaseData = data })
                       .ToList()
                       .ForEach(layout.Connections.Add);

            return layout;
        }
    }
}
