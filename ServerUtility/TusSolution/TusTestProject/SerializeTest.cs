using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Tus.TransControl.Base;

namespace TestProject
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class SerializeTest
    {
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void VehicleSerializeTest()
        {
            var cnt = new DataContractJsonSerializer(typeof (Vehicle));

            using(var ms = new MemoryStream())
            using (var sr = new StreamReader(ms))
            {

                ms.Position = 0;
            }
        }

    }
}
