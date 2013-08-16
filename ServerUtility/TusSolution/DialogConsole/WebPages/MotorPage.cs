using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;

namespace DialogConsole.WebPages
{
    [Export(typeof(IConsolePage))]
    [TusPageMetadata("motor device control", "motor")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MotorPage : ConsolePageBase<IEnumerable<Motor>>
    {
        protected override System.Runtime.Serialization.Json.DataContractJsonSerializer JsonSerializer
        {
            get
            {
                return new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(IEnumerable<Motor>),
                    new[] { typeof(MemoryState) });
            }
        }

        private IEnumerable<Motor> motors
        {
            get
            {
                return this.Param.UsingLayout.Sheet.AllDevices.Where(d => d.ModuleType == Tus.Communication.ModuleTypeEnum.AvrMotor)
                    .Cast<Motor>();
            }
        }

        public override IEnumerable<Motor> GetContent()
        {
            return motors;
        }
        public override void ApplyJsonRequest(IEnumerable<Motor> obj)
        {
            throw new NotImplementedException();
        }

    }
}