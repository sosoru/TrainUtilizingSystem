using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LibUsbDotNet;
using LibUsbDotNet.Main;

using System.Reactive.Linq;
using System.Reactive.Concurrency;

namespace SensorLibrary.Packet.IO
{
    public class USBDeviceController : IDeviceIO
    {
        public UsbDevice Device { get; private set; }
        protected UsbEndpointReader Reader { get; private set; }
        protected UsbEndpointWriter Writer { get; private set; }

        private volatile object Lockpacketlist = new object();
        private Queue<DevicePacket> packetlist = new Queue<DevicePacket>();

        public USBDeviceController(UsbDevice dev)
            : base()
        {
            this.Device = dev;

        }

        public void Open()
        {
            Device.Open();

            Reader = Device.OpenEndpointReader(ReadEndpointID.Ep01, 1024, EndpointType.Bulk);

            //Reader.Reset();

            Reader.DataReceivedEnabled = true;
            Reader.DataReceived += (sender, e)
                =>
                {
                    System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            using (var ms = new MemoryStream(e.Buffer))
                            {
                                do
                                {
                                    var buf = new byte [32];

                                    ms.Read(buf, 0, 32);

                                    var packet = buf.ToDevicePacket();

                                    if (true)//packet.ReadMark == 0xFF)
                                    {
                                        lock (Lockpacketlist)
                                            this.packetlist.Enqueue(packet);
                                    }

                                } while (e.Count >= ms.Position + 32);
                            }
                        });
                };

            Writer = Device.OpenEndpointWriter(WriteEndpointID.Ep01);
            //Writer.Reset();
        }

        public IObservable<DevicePacket> CreateDevicePacketObservable()
        {
            var q = Observable.FromEventPattern(this.Reader, "DataReceived")
                              .SelectMany(args =>
                                  {
                                      var list = new List<DevicePacket>();
                                      lock (Lockpacketlist)
                                          while (this.packetlist.Peek() != null)
                                              list.Add(this.packetlist.Dequeue());
                                      return list;
                                  });

            return q;
        }

        public DevicePacket ReadPacket()
        {
            lock (Lockpacketlist)
            {
                if (this.packetlist.Count > 0)
                    return this.packetlist.Dequeue();
                else
                    return null;
            }
        }

        public void WritePacket(DevicePacket packet)
        {
            var buf = new byte [64];

            packet.ToByteArray().CopyTo(buf, 0);
            this.Write(buf, 0, buf.Length);
        }

        public void Write(byte [] buffer, int offset, int count)
        {
            int pos = 0;

            do
            {
                var trans = new byte [Writer.EndpointInfo.Descriptor.MaxPacketSize];
                var len = 0;
                Array.Copy(buffer, offset + pos, trans, 0, trans.Length);

                var ec = this.Writer.Write(trans, 500, out len);
                if (ec != ErrorCode.None)
                {
                    this.usbReset();

                    ec = this.Writer.Write(trans, 500, out len);

                    if (ec != ErrorCode.None)
                        throw new IOException(UsbDevice.LastErrorString);
                }

                pos += len;

            } while (pos < count);

            return;
        }

        private void usbReset()
        {
            if (this.Reader == null || this.Device == null)
                return;

            if (!this.Reader.IsDisposed)
                this.Reader.Dispose();

            if (this.Device.IsOpen)
                this.Device.Close();

            this.Open();
        }


        public IObservable<DevicePacket> GetReadingPacket()
        {
            throw new NotImplementedException();
        }


        public IObservable<DevicePacket> GetWritingPacket(DevicePacket pack)
        {
            throw new NotImplementedException();
        }
    }

}
