using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DialogConsole;
using DialogConsole.Factory;

namespace TestProject.ConsoleTest.Composition
{
    [TestClass]
    public class SheetFactoryTest 
    {
        [TestMethod]
        public void CreateSheet()
       {
           var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(".\DialogConsole.exe"));
                var container = new CompositionContainer(catalog);
            container.GetExport<

        }
    }
}
