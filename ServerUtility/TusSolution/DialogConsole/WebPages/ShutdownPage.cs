using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tus.Diagnostics;

namespace DialogConsole.WebPages
{
    [DataContract]
    class ShutdownInfoReceived
    {
        [DataMember(IsRequired = true)]
        public string code;
    }

    [Export(typeof(IConsolePage))]
    [TusPageMetadata("emergency shutdown", "shutdown")]
    class ShutdownPage : ConsolePageBase<object, ShutdownInfoReceived>
    {
        protected override IEnumerable<Type> KnownTypesWhenSerialization
        {
            get
            {
                return new[]
                           {typeof(ShutdownInfoReceived)
                           };
            }
        }

        public override object CreateSendingContent()
        {
            return new object();
        }

        public override void ApplyReceivedJsonRequest()
        {
            ShutdownInfoReceived obj;
            if (!this.ReceivedContents.TryDequeue(out obj)) return;

            // 列車停止命令
            if (obj.code == "vehicles")
            {
                var vehicles = this.Param.UsingLayout.Vehicles;
                foreach (var v in vehicles)
                {
                    v.Accelation = 0.8f;
                    v.Speed = 0.0f;

                    Logger.WriteLineAsWebInfo("Shutdown All Vehicles");
                }
            }
        }
    }
}
