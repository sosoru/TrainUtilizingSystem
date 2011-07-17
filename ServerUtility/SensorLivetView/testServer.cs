using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;
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

        public TestEnumerable SetMotherBoard(DeviceID id)
        {
            var packet = new DevicePacket()
            {
                ID = id,
                ModuleType = ModuleTypeEnum.MotherBoard,
            };
            var state = new MotherBoardState(packet);
            for (int i = 0; i < state.ModuleTypeLength; i++)
            {
                if (i == 0)
                    state[i] = ModuleTypeEnum.MotherBoard;
                else if (i < 5)
                    state[i] = ModuleTypeEnum.TrainSensor;
                else if (i == 5)
                    state[i] = ModuleTypeEnum.PointModule;

            }

            this.stacking.Add(state.BasePacket);
            return new TestEnumerable(this);
        }

        public TestEnumerable SetTrainSensors(DeviceID id)
        {
            var tsenses = Enumerable.Range(0, 256)
                                    .Select((i) => new TrainSensorState(
                                            new DevicePacket()
                                            {
                                                ID = id,
                                                ModuleType = ModuleTypeEnum.TrainSensor,

                                            }, null, null)
                                            {
                                                Mode = TrainSensorMode.meisuring,
                                                ReferenceVoltageMinus = 0.0F,
                                                ReferenceVoltagePlus = 5.0F,
                                                VoltageResolution = 10,
                                                ThresholdVoltage = 2.0F,
                                                CurrentVoltage = (float)(5.0 * (double)i / 256.0),
                                                Timer = (ushort)(i * 256),
                                            });
            this.stacking.AddRange(tsenses.Select((t) => t.BasePacket));
            return new TestEnumerable(this);
        }

        public IEnumerable<DevicePacket> ToEnumerable()
        {
            return this.stacking;
        }

    }

    public class TestServer
        : PacketServer
    {
        public IEnumerable<DevicePacket> SendingPackets { get; set; }

        public TestServer(IEnumerable<DevicePacket> en)
            : base(new TestPacketStream(en))
        {
            this.SendingPackets = en;
        }

        public void ResetStream()
        {
            this.BaseStream.Close();
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
            get { return false; }
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

        public override int Read(byte[] buffer, int offset, int count)
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

                buffer[pos + offset] = current[currentPos++];

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

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
