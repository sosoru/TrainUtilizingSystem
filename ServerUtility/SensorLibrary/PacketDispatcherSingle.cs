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
        public ObservableCollection<MotherBoard> ReceivedMotherBoardList { get; private set; }
        public event EventHandler ReceivedMotherBoardChanged;

        public PacketDispatcherSingle()
            : base()
        {
            this.ReceivedMotherBoardList = new ObservableCollection<MotherBoard>();
        }

        public override void Notify(IDeviceState<IPacketDeviceData> state)
        {
            if (state.BasePacket.ModuleType == ModuleTypeEnum.MotherBoard)
            {
                var casted = state as MotherBoardState;
                if (casted == null)
                    throw new InvalidCastException("mismatching type of DevciceState and one of ModuleType");

                var curmb = new MotherBoard() { DeviceID = state.BasePacket.ID };
                var comp = new MotherBoardStateComparer();

                var before = this.ReceivedMotherBoardList.FirstOrDefault((mb) => mb.DeviceID == curmb.DeviceID);

                if (before == null)
                {
                    curmb.Observe(this);
                    this.ReceivedMotherBoardList.Add(curmb);
                    before = curmb;
                }

                if (!comp.Equals(before.CurrentState, casted))
                {
                    this.Refresh(casted);
                }
                OnReceivedMotherBoardChanged();
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
            if (this.ReceivedMotherBoardList == null)
                return;

            foreach (var mb in this.ReceivedMotherBoardList)
                this.Refresh(mb.CurrentState);
        }

        private void Refresh(MotherBoardState state)
        {
            if (state == null)
                return;

            var list = this._cache_availabledevs;
            foreach (var dev in state.DeviceEnumerate())
            {
                list.Add(dev);
                dev.Observe(this);
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
