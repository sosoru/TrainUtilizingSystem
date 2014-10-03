using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tus.Illumination;

namespace TusTestProject.FileParseTest
{
    [TestClass]
    [DeploymentItem("SampleLayout/katsushika_illuminate.yaml")]
    public class IlluminateYamlTest
    {
        [TestMethod]
        public void ParseFromFileTest()
        {
            var target = new IlluminativeObjectFactory("katsushika_illuminate.yaml", null);
            var result = target.Create().ToArray();
        }

    }
}
