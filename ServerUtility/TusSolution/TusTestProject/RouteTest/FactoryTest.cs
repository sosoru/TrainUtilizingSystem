using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tus.Communication;
using Tus.Factory;
using Tus.TransControl.Base;
using Tus.TransControl.Parser;

namespace TestProject
{
    [DeploymentItem("SampleLayout/test_layout_route.yaml")]
    [DeploymentItem("SampleLayout/test_looping.yaml")]
    [TestClass]
    public class FactoryTest
    {
        private BlockSheet sheet
        {
            get
            {
                var yaml = new BlockYaml();
                var info = yaml.Parse("test_looping.yaml");
                var serv = new PacketServer();
                var sheet = new BlockSheet(info, serv);
                return sheet;
            }
        }

        private IConsoleApplicationSettings Settings
        {
            get
            {
                var mock = new Mock<IConsoleApplicationSettings>();
                mock.Setup(m => m.RoutePath).Returns("test_layout_route.yaml");
                return mock.Object;
            }
        }

        [TestMethod]
        public void CreateReverseTest()
        {
            var rtfact = new RouteOrderListFactory();
            rtfact.Sheet = sheet;
            rtfact.ApplicationSettings = Settings;

            var routes = rtfact.Create();
            var posrt = routes.First();
            var revrt = rtfact.ReverseRoute(posrt);

            Console.WriteLine(posrt.ToString());
            Console.WriteLine(revrt.ToString());

        }
    }
}
