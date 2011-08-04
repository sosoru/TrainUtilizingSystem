using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reactive.Linq;

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
                if (ReceivedMotherBoard == null || !new MotherBoardStateComparer().Equals(state as MotherBoardState, this.ReceivedMotherBoard.CurrentState))
                {
                    mb.Observe(this);
                    this.ReceivedMotherBoard = mb;
                    this.Refresh(state as MotherBoardState);
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


        private ObservableCollection<IDevice<IDeviceState<IPacketDeviceData>>> _cache_availabledevs
            = new ObservableCollection<IDevice<IDeviceState<IPacketDeviceData>>>();
        public ObservableCollection<IDevice<IDeviceState<IPacketDeviceData>>> AvailableDevices
        {
            get
            {
                return _cache_availabledevs;
            }
        }

        public void Refresh()
        {
            if (this.ReceivedMotherBoard == null)
                return;

            this.Refresh(this.ReceivedMotherBoard.CurrentState);
        }

        private void Refresh(MotherBoardState state)
        {
            if (state == null)
                return;

            var len = state.ModuleTypeLength;
            var list = this._cache_availabledevs;
            for (byte i = 0; i < len; i++)
            {
                var devtype = state [i];
                var dev = PacketDispatcher.GetCorrectDevice(devtype, new DeviceID(this.ReceivedMotherBoard.DeviceID.ParentPart, i));
                if (dev != null)
                {
                    list.Add(dev);
                    dev.Observe(this);
                }
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
