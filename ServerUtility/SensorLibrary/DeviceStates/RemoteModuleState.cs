using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorLibrary
{
    public class RemoteModuleState
        : DeviceState<RemoteModuleData>
    {
        public RemoteModuleState()
            : base()
        {
        }

        public DeviceID RemotingID
        {
            get
            {
                return this.Data.remoteid;
            }
            set
            {
                this.Data.remoteid = value;
            }
        }
    }
}
