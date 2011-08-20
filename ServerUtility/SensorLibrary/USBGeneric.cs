using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace SensorLibrary
{
    public class USBStream
        : Stream
    {
        public UsbDevice Device { get; private set; }
        protected UsbEndpointReader Reader { get; private set; }
        protected UsbEndpointWriter Writer { get; private set; }

        public USBStream(UsbDevice dev)
            : base()
        {
            this.Device = dev;
            this.ReadTimeout = 500;
            this.WriteTimeout = 500;
        }

        public void Open()
        {
            Device.Open();

            Reader = Device.OpenEndpointReader(ReadEndpointID.Ep01, 4096, EndpointType.Bulk);

            Writer = Device.OpenEndpointWriter(WriteEndpointID.Ep01);

            Reader.Reset();
        }

        public override bool CanRead
        {
            get { return this.Device.IsOpen; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return this.Device.IsOpen; }
        }

        public override bool CanTimeout
        {
            get
            {
                return true;
            }
        }

        public override int ReadTimeout { get; set; }
        public override int WriteTimeout { get; set; }

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
            int pos = 0;
            do
            {
                int len = 64;
                var buf = new byte [len];
                // we should read data per 64byte and implement double buffer

                var ec = this.Reader.Read(buf, this.ReadTimeout, out len);
                if (ec != ErrorCode.None)
                    throw new IOException(Enum.GetName(typeof(ErrorCode), ec));

                Array.Copy(buf, 0, buffer, pos + offset, len);
                pos += len;
            } while (pos == 0);

            return pos;
        }

        public DevicePacket ReadPacket()
        {
            var pack = new DevicePacket();
            var buffer = new byte [32];

            this.Read(buffer, 0, buffer.Length);

            using (var ms = new MemoryStream(buffer))
            using (var sr = new BinaryReader(ms))
            {
                var mark = sr.ReadByte();
                pack.ID = new DeviceID()
                {
                    ParentPart = sr.ReadByte(),
                    ModulePart = sr.ReadByte(),
                };
                pack.ModuleType = (ModuleTypeEnum)sr.ReadByte();
                sr.ReadBytes(28).CopyTo(pack.Data, 0);
            }
            return pack;
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
            int pos = 0;

            do
            {
                var trans = new byte [Writer.EndpointInfo.Descriptor.MaxPacketSize];
                var len = 0;
                Array.Copy(buffer, offset + pos, trans, 0, trans.Length);

                var ec = this.Writer.Write(trans, this.WriteTimeout, out len);
                if (ec != ErrorCode.None)
                    throw new IOException(UsbDevice.LastErrorString);

                pos += len;

            } while (pos < count);

            return;
        }
    }

}
