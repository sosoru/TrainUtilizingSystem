using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;
using System.IO;

namespace TrainControllerServer
{
    public class TestServer
        : PacketServer
    {

        public TestServer(IEnumerable<DevicePacket> en)
            : base(new TestPacketStream(en))
        { 
            
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
