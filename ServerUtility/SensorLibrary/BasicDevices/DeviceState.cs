using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace SensorLibrary
{
    public interface IDeviceState<out T>
    {
        DevicePacket BasePacket { get; set; }
        PacketServer ReceivingServer { get; set; }
        T Data { get; }
    }

    public class DeviceState<T>
        : IDeviceState<T>
        where T : IPacketDeviceData
    {
        private T _deviceStateCache;

        private DevicePacket _basepacket;
        public DevicePacket BasePacket
        {
            get
            {
                this.FlushDataState();
                return this._basepacket;
            }
            set
            {
                this._basepacket = value;
            }
        }
        public PacketServer ReceivingServer { get; set; }

        public DeviceState()
        {
        }

        public override string ToString()
        {
            return this.BasePacket.ToString();
        }

        private volatile object lock_Data = new object();
        public T Data
        {
            get
            {
                lock (lock_Data)
                {
                    if (_deviceStateCache == null)
                    {
                        var state = this._basepacket.CopyFromData<T>();
                        this._deviceStateCache = state;
                    }
                    return this._deviceStateCache;
                }
            }
            set
            {
                this._deviceStateCache = value;
            }
        }

        public void FlushDataState()
        {
            if (this._deviceStateCache != null)
            {
                this._basepacket.CopyToData<T>(this._deviceStateCache);
                this._deviceStateCache = default(T);
            }
        }

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
