using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using DialogConsole.Features.Base;
using DialogConsole.WebPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tus.Communication;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;
using Tus.TransControl.Parser;

namespace TestProject.ConsoleTest.Composition
{
    [DeploymentItem("SampleLayout/middletrack.yaml")]
    [TestClass]
    public class SwitchPageTest
    {
        private IFeatureParameters SampleParams
        {
            get
            {
                var yaml = new BlockYaml();
                var blocks = yaml.Parse("middletrack.yaml");
                var sheet = new BlockSheet(blocks, new PacketServer());

                var mock = new Mock<IFeatureParameters>();
                mock.Setup(param => param.Sheet).Returns(() => sheet);

                var vehicle = new Vehicle(sheet, new Route(sheet, new[] { "AT1" }));
                mock.Setup(param => param.Vehicles).Returns(() => new[] { vehicle });

                return mock.Object;
            }
        }

        [TestMethod]
        public void GetSwitchContextTest()
        {
            var page = new SwitchPage();
            //page.Serializer = new DataContractJsonSerializer(typeof(IEnumerable<Switch>));
            page.Param = this.SampleParams;

            var result = page.GetJsonContent();
            Console.WriteLine(result);
        }

        [TestMethod]
        public void GetVehicleContextTest()
        {
            var page = new VehiclePage();
            page.Param = this.SampleParams;

            var result = page.GetJsonContent();
        }

        [TestMethod]
        public void GetMotorContextTest()
        {
            var page = new MotorPage();
            page.Param = this.SampleParams;

            var result = page.GetJsonContent();
        }

        [TestMethod]
        public void GetBlockConntextTest()
        {
            var page = new BlockPage();
            page.Param = this.SampleParams;

            var result = page.GetJsonContent();
            Console.WriteLine(result);
        }
    }
}
