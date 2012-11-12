using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

using SensorLibrary.Packet.Control;

namespace SensorLibrary.Devices
{
    public interface IDeviceState<out T>
    {
        //DevicePacket BasePacket { get; set; }
        DeviceID ID { get; set; }
        ModuleTypeEnum ModuleType { get; set; }
        T Data { get; }
    }

    public class DeviceState<T>
        : IDeviceState<T>
        where T : IPacketDeviceData, new()
    {
        private T _deviceStateCache;

        //private DevicePacket _basepacket;
        //public DevicePacket BasePacket
        //{
        //    get
        //    {
        //        this.FlushDataState();
        //        return this._basepacket;
        //    }
        //    set
        //    {
        //        this._basepacket = value;
        //        this._deviceStateCache = default(T);
        //    }
        //}

        public DeviceState()
        {
        }

        public ModuleTypeEnum ModuleType
        {
            get { return (ModuleTypeEnum)this.Data.ModuleType; }
            set { this.Data.ModuleType = (byte)value; }
        }

        public DeviceID ID { get; set; }

        //public override string ToString()
        //{
        //    return this.BasePacket.ToString();
        //}

        private volatile object lock_Data = new object();
        public T Data
        {
            get
            {
                lock (lock_Data)
                {
                    if (_deviceStateCache == null)
                    {
                        var state = new T(); //this._basepacket.CopyFromData<T>();
                        this._deviceStateCache = state;
                    }
                    return this._deviceStateCache;
                }
            }
            set
            {
                lock (lock_Data)
                    this._deviceStateCache = value;
            }
        }

        public override string ToString()
        {
            return this.ID.ToString();
        }

        //public void FlushDataState()
        //{
        //    lock (lock_Data)
        //    {
        //        if (this._deviceStateCache != null)
        //        {
        //            this._basepacket.CopyToData<T>(this._deviceStateCache);
        //            this._deviceStateCache = default(T);
        //        }

        //    }
        //}

        //public static IDeviceState<IPacketDeviceData> CreateCorrectState(DevicePacket pack, PacketServer server)
        //{
        //    IDeviceState<IPacketDeviceData> state;
        //    switch (pack.ModuleType)
        //    {
        //        case ModuleTypeEnum.MotherBoard:
        //            state = new MotherBoardState(pack, null, server);
        //            break;
        //        case ModuleTypeEnum.TrainSensor:
        //            state = new TrainSensorState(pack, null, server);
        //            break;
        //        case ModuleTypeEnum.PointModule:
        //            state = new PointModuleState(pack, null, server);
        //            break;
        //        case ModuleTypeEnum.TrainController:
        //            state = new TrainControllerState(pack, null, server);
        //            break;
        //        case ModuleTypeEnum.RemoteModule:
        //            state = new RemoteModuleState(pack, null, server);
        //            break;
        //        default:
        //            throw new ArgumentException("Invalid Packet");
        //    }

        //return state;

        //}

    }


}
