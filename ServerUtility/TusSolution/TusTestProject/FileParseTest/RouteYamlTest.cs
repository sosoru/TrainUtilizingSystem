using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tus.TransControl.Parser;
using Tus.TransControl.Parser;

namespace TestProject.FileParseTest
{
    [TestClass]
    public class RouteYamlTest
    {
        string SimpleRouteYaml
        {
            get
            {
                return @"- &rt_seg
   AT1, AT2
-
  name : AT
  polar : pos
  route : 
    - *rt_seg
    - *rt_seg";
            }
        }
        //[TestMethod]
        //public void RouteYamlParseFromTest()
        //{
        //    var target = new RouteYaml();
        //    target.ParseFrom(@"C:\Users\Administrator\Desktop\layout - コピー.yaml");
        //}

        [TestMethod]
        public void RouteYamlContentParseTest()
        {
            var target = new RouteYaml();
            var objs = target.ParseFromContent(SimpleRouteYaml);

            var sequence = target.ParseYamlContent(objs);

            var resultseq = sequence.First();
            Assert.AreEqual("AT", resultseq.Name);

            var resultarr = resultseq.Routes.ToArray();
            Assert.AreEqual("AT1", resultarr[0]);
            Assert.AreEqual("AT2", resultarr[1]);
            Assert.AreEqual("AT1", resultarr[2]);
            Assert.AreEqual("AT2", resultarr[3]);

        }
    }
}
