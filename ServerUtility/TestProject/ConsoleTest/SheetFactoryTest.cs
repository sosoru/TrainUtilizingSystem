using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DialogConsole;
using DialogConsole.Factory;

namespace TestProject.ConsoleTest
{
    [TestClass]
    public class SheetFactoryTest 
    {
        [TestMethod]
        public void CreateSheet()
       {
           var path = "dummy";
           var f = new SheetFactory(path);

        }
    }
}
