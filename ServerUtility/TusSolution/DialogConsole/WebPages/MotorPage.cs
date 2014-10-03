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
    public class MotorPage : ConsolePageBase<IEnumerable<Motor>, IEnumerable<Motor>>
    {
        protected override IEnumerable<Type> KnownTypesWhenSerialization
        {
            get
            {
                return new[] { typeof(MemoryState) };
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

        public override IEnumerable<Motor> CreateSendingContent()
        {
            return motors;
        }
        public override void ApplyReceivedJsonRequest()
        {
        }

    }
}