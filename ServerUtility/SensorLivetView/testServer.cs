using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;
using SensorLibrary.Packet;
using SensorLibrary.Packet.IO;
using SensorLibrary.Packet.Data;
using SensorLibrary.Devices;
using SensorLibrary.Devices.PicUsbDevices;

using System.IO;

namespace SensorLivetView
{
    public class TestEnumerable
    {

        private List<DevicePacket> _cache_stacking;
        private List<DevicePacket> stacking
        {
            get
            {
                return this._cache_stacking ?? (_cache_stacking = new List<DevicePacket>());
            }
        }

        private TestEnumerable(TestEnumerable cascade)
        {
            this._cache_stacking = cascade._cache_stacking;
        }

        public TestEnumerable() { }

        private TestEnumerable setStack(Func<IEnumerable<IDeviceState<IPacketDeviceData>>> f)
        {
            this.stacking.AddRange(f().Select((state) => state.BasePacket));
            return new TestEnumerable(this);
        }

        public TestEnumerable SetMotherBoard(DeviceID id)
        {
            var packet = new DevicePacket()
            {
                ID = id,
                ModuleType = ModuleTypeEnum.MotherBoard,
            };
            var state = PicDeviceFactoryProvider.MotherBoardFactory.DeviceStateCreate();
            state.BasePacket = packet;
            state.Data.ModuleType [0] = 0x10; //mb, sens

            return setStack(() => new [] { state });
        }

        public TestEnumerable SetTrainSensors(DeviceID id)
        {
            var tsenses = Enumerable.Range(0, 256)
                                    .Select((i) => new TrainSensorState()
                                              {
                                                  BasePacket = new DevicePacket()
                                                  {
                                                      ID = id,
                                                      ModuleType = ModuleTypeEnum.TrainSensor,
                                                  },
                                                  Mode = TrainSensorMode.meisuring,
                                                  ReferenceVoltageMinus = 0.0F,
                                                  ReferenceVoltagePlus = 5.0F,
                                                  VoltageResolution = 10,
                                                  ThresholdVoltageLower = 2.0F,
                                                  CurrentVoltage = (float)(5.0 * (double)i / 256.0),
                                                  Timer = (ushort)(i * 256),
                                              });
            return setStack(() => tsenses);
        }

        public TestEnumerable SetTrainDetectingSensor(DeviceID id)
        {
            var tsens = Enumerable.Range(0, 256)
                                  .Select((i) => new TrainSensorState()
                                  {
                                      BasePacket = new DevicePacket()
                                      {
                                          ID = id,
                                          ModuleType = ModuleTypeEnum.TrainSensor,
                                      },
                                      Mode = TrainSensorMode.detecting,
                                      ReferenceVoltageMinus = 0.0f,
                                      ReferenceVoltagePlus = 5.0f,
                                      VoltageResolution = 10,
                                      ThresholdVoltageLower = 2.5F,
                                      ThresholdVoltageHigher = 4.0f,
                                      CurrentVoltage = i * 2.5f,
                                      Timer = (ushort)(i * 1000),
                                      IsDetected = i == 0,
                                  });
            return setStack(() => tsens);

        }

        public TestEnumerable SetPointModules(DeviceID id)
        {
            var data = new PointModuleData();
            for (int i = 0; i < data.Directions.Length; i += 2)
                data.Directions [i] = 1;

            var state = new PointModuleState()
            {
                BasePacket = new DevicePacket() { ID = id, ModuleType = ModuleTypeEnum.PointModule },
                Data = data,
            };

            return setStack(() => new [] { state });
        }

        public TestEnumerable SetController(DeviceID id)
        {
            var data  = new TrainControllerData();

            data.mode = TrainControllerMode.Duty;
            data.duty = 0;
            data.direction = TrainControllerDirection.Positive;
            data.paramp = 0xcc;
            data.parami = 0x11;
            
            var stat = new TrainControllerState()
            {
                BasePacket = new DevicePacket() { ID = id, ModuleType = ModuleTypeEnum.TrainController },
                Data = data,
            };

            return setStack(() => new [] { stat });
        }

        public IEnumerable<DevicePacket> ToEnumerable()
        {
            return this.stacking;
        }

    }

    public class DeviceIoByEnumerable
        : IDeviceIO
    {
        private IEnumerable<DevicePacket> packets;
        private IEnumerator<DevicePacket> enumerator;

        public DeviceIoByEnumerable(IEnumerable<DevicePacket> ie)
        {
            this.packets = ie;

            this.enumerator = this.packets.GetEnumerator();
        }

        public DevicePacket ReadPacket()
        {
            if (this.enumerator.MoveNext())
                return this.enumerator.Current;
            else
            {
                this.enumerator.Reset();
                return this.ReadPacket();
            }

        }

        public void WritePacket(DevicePacket packet)
        {
            // do nothing
        }
    }

    public class TestPacketStream
        : Stream
    {
        private IEnumerable<DevicePacket> buf;
        private IEnumerator<DevicePacket> enumerator;
        private byte[] current;
        private int currentPos;

        public TestPacketStream(IEnumerable<DevicePacket> en)
        {
            buf = en;
            enumerator = en.GetEnumerator();
        }

        public Action<byte []> WriteFunc { get; set; }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int Read(byte [] buffer, int offset, int count)
        {
            int pos;
            for (pos = 0; pos < count; pos++)
            {
                if (current == null)
                {
                    currentPos = 0;
                    if (this.enumerator.MoveNext())
                        current = this.enumerator.Current.ToByteArray();
                    else
                    {
                        this.enumerator.Reset();
                        this.enumerator.MoveNext();
                        current = this.enumerator.Current.ToByteArray();

                    }
                }

                buffer [pos + offset] = current [currentPos++];

                if (currentPos == current.Length)
                    current = null;
            }
            System.Threading.Thread.Sleep(1);
            return pos;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte [] buffer, int offset, int count)
        {
            if (this.WriteFunc != null)
                this.WriteFunc(buffer.Skip(offset).Take(count).ToArray());
        }
    }
}
