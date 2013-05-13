using System.Linq;
using System.Runtime.Serialization;

namespace Tus.TransControl.Base
{
    public enum SensingMethod
    {
        IRComming,
    }

    [DataContract]
    public class Halt
    {
        [DataMember]
        public virtual Block HaltBlock { get; set; }
        [DataMember]
        public SensingMethod Method { get; set; }

        public Halt(Block blk)
        {
            this.HaltBlock = blk;
            this.Method = SensingMethod.IRComming;
        }
        public Halt() { }

        public bool IRSensingHalt()
        {
            if (!this.HaltBlock.HasSensor)
                return true;

            var sens = this.HaltBlock.Detector;
            return sens.Devices.Any(s => s.IsDetected);
        }

        public virtual bool HaltState
        {
            get
            {
                return IRSensingHalt();
            }
        }
    }
}
