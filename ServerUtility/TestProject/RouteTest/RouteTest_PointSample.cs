using RouteLibrary.Base;
using RouteLibrary.Parser;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Data;
using SensorLibrary.Packet.IO;
using SensorLibrary.Packet.Control;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Moq;
using Moq.Linq;
using SensorLibrary;
using System.Reactive.Subjects;

namespace TestProject
{
    [TestClass]
    public class RouteTest_PointSample
    {

        IEnumerable<BlockInfo> sample_point_sheet
        {
            get
            {
                var yaml = new BlockYaml();
                var blocks = yaml.Parse("./SampleLayout/point_sample.yaml");

                return blocks;
            }
        }

        Route GetPositiveRoute(BlockSheet sht)
        {
            var route = new Route(sht, new [] { "AT1", "AT2", "BT3", "AT4", "AT5"});
            
            route.AddStopInfo(sht.GetBlock("BT3"));

            return route;
        }

        [TestMethod]
        public void ReadTestSheet()
        {
            var sht = sample_point_sheet.ToArray();

        }
        
        [TestMethod]
        public void CreateRouteTest()
        {
            var serv = new PacketServer(new AvrDeviceFactoryProvider());
            var sht = new BlockSheet(sample_point_sheet, serv);

            var route = GetPositiveRoute(sht);

        }

        [TestMethod]
        public void ApplyRouteTest_ForCurvedPath()
        {
            var serv = new PacketServer(new AvrDeviceFactoryProvider());
            var disp = serv.GetDispatcher();
            var writtenstate = new List<IDeviceState<IPacketDeviceData>>();

            disp.Subscribe(state => writtenstate.Add(state));

            var sht = new BlockSheet(sample_point_sheet, serv);
            var route = GetPositiveRoute(sht);
            var cmd = new CommandInfo()
            {
                Route = route,
                Speed = 0.5f
            };

            //1 : detected at AT1
            writtenstate.Clear();
            

            
            
        }
    }
}
