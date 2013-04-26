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

                return mock.Object;
            }
        }

        [TestMethod]
        public void GetContextTest()
        {
            var page = new ConsolePage();
            //page.Serializer = new DataContractJsonSerializer(typeof(IEnumerable<Switch>));
            page.Param = this.SampleParams;

            var result = page.GetJsonContext();
        }
    }
}
