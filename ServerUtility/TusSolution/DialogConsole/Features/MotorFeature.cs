using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DialogConsole.Features.Base;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;

namespace DialogConsole.Features
{
    [FeatureMetadata("9", "motor")]
    [Export(typeof(IFeature))]
    internal class MotorFeature
        : BaseFeature, IFeature
    {
        public void Init()
        {
        }

        private IDisposable _before = null;

        public void Execute()
        {
            Console.WriteLine("input blockname");
            var blkname = Console.ReadLine();

            var blk = this.Param.UsingLayout.Sheet.GetBlock(blkname);
            if(blk == null)
            {
                      Console.WriteLine("block is not found");
                      return;
            }
            if (!blk.HasMotor)
            {
                                Console.WriteLine("this block doesnot have motor");
                                return;
            }

            foreach (
                var dev in
                    this.Param.UsingLayout.Sheet.AllDevices.Where(
                        d => d.ModuleType == Tus.Communication.ModuleTypeEnum.AvrMotor)
                        .Cast<Motor>())
            {
                dev.CurrentState.Duty = 0.0f;
                dev.SendState();
            }
            
            if (_before != null)
                _before.Dispose();

            _before = Observable.Defer(() => Observable.Start(() => ApplyToMotor(blk)))
                .Delay(TimeSpan.FromMilliseconds(500))
                .Repeat()
                .Subscribe();

        }

        private static void ApplyToMotor(Block blk)
        {
            foreach (var dev in blk.MotorEffector.Devices)
            {
                dev.CurrentState.Duty = 1.0f;
                dev.SendState();
            }
        }
    }
}
