using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tus.Factory;

namespace TestProject.ConsoleTest.Composition
{
    [TestClass]
    public class SheetFactoryTest
    {
        [TestMethod]
        public void CreateSheet()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(".\\Tus.Factory.dll"));
            var container = new CompositionContainer(catalog);
            container.GetExport<Tus.Factory.SheetFactory>();
            var result = container.GetExport<SheetFactory>();

            var val = result.Value.Create();
        }
    }
}
