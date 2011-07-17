using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SensorLibrary
{
    public class PacketDispatcherSingle
        : PacketDispatcher
    {
        public MotherBoard ReceivedMotherBoard { get; private set; }
        public event EventHandler ReceivedMotherBoardChanged;

        public override void Notify(IDeviceState<IPacketDeviceData> state)
        {
            if (state.BasePacket.ModuleType == ModuleTypeEnum.MotherBoard)
            {
                var mb = new MotherBoard(state.BasePacket.ID);
                if (ReceivedMotherBoard == null || this.ReceivedMotherBoard.DeviceID != state.BasePacket.ID)
                {
                    mb.Observe(this);
                    this.ReceivedMotherBoard = mb;
                    OnReceivedMotherBoardChanged();
                }
            }

            base.Notify(state);
        }

        public IList<IDevice<IDeviceState<IPacketDeviceData>>> CacheDevices()
        {
            var list = this.AvailableDevices;
            return list;
        }


        private IList<IDevice<IDeviceState<IPacketDeviceData>>> _cache_availabledevs = null;
        public IList<IDevice<IDeviceState<IPacketDeviceData>>> AvailableDevices
        {
            get
            {
                if (this.ReceivedMotherBoard == null && this.ReceivedMotherBoard.CurrentState == null)
                    return new IDevice<IDeviceState<IPacketDeviceData>>[0];

                if (_cache_availabledevs == null)
                {
                    var len = this.ReceivedMotherBoard.CurrentState.ModuleTypeLength;
                    var list = new List<IDevice<IDeviceState<IPacketDeviceData>>>();
                    for (byte i = 0; i < len; i++)
                    {
                        var devtype = this.ReceivedMotherBoard.CurrentState[i];
                        var dev = PacketDispatcher.GetCorrectDevice(devtype, new DeviceID(this.ReceivedMotherBoard.DeviceID.ParentPart, i));
                        if (dev != null)
                        {
                            list.Add(dev);
                            dev.Observe(this);
                        }
                    }
                    this._cache_availabledevs = list;
                }
                return _cache_availabledevs;
            }
        }

        public IEnumerable<T> GetCastedAilveDevices<T>()
        {
            return this.AvailableDevices.Where((dev) => dev is T)
                                        .Cast<T>();
        }


        //public override TDevice GetDevice<TDevice>(DeviceID id)
        //{
        //    TDevice dev = null;
        //    try
        //    {
        //        dev = this.AvailableDevices.First((d) => d.DeviceID == id) as TDevice;
        //    }
        //    catch (InvalidOperationException) { }

        //    return dev;
        //}

        protected void OnReceivedMotherBoardChanged()
        {
            if (this.ReceivedMotherBoardChanged != null)
            {
                this.ReceivedMotherBoardChanged(this, new EventArgs());
            }
        }
    }
}
