using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tus.Factory;
using DialogConsole;

namespace TestProject.ConsoleTest.Composition
{
    [TestClass]
    [DeploymentItem("SampleLayout/815.yaml")]
    public class FactoryTest
    {
        [Export(typeof(IConsoleApplicationSettings))]
        private class TestConsoleSetting
            : IConsoleApplicationSettings
        {
            public string IpMask
            {
                get { return "255.255.255.0"; }
            }

            public int IpPort
            {
                get { return 8000; }
            }

            public string IpSegment
            {
                get { return "192.168.2.1"; }
            }

            public string ParentDeviceID
            {
                get { return "(1,1,1)"; }
            }

            public string SheetPath
            {
                get { return @"815.yaml"; }
            }
            public string RoutePath
            { get { return @"test_layout_route.yaml"; } }
        }

        [TestMethod]
        public void CreateSheet()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(".\\Tus.Factory.dll"));
            catalog.Catalogs.Add(new TypeCatalog(typeof(TestConsoleSetting)));
            var container = new CompositionContainer(catalog);
            container.GetExport<Tus.Factory.SheetFactory>();
            var result = container.GetExport<SheetFactory>();

            var val = result.Value.Create();
        }

        [TestMethod]
        public void CreateDialogConsole()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(".\\Tus.Factory.dll"));
            //catalog.Catalogs.Add(new TypeCatalog(typeof(TestConsoleSetting)));
            catalog.Catalogs.Add(new AssemblyCatalog(".\\DialogConsole.exe"));
            var container = new CompositionContainer(catalog);
            //var shfact = container.GetExport<RouteListFactory>();

            var result = container.GetExport<DialogConsoleClass>();
        }

    }
}
