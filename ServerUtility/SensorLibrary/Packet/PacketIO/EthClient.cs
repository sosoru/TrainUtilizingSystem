using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive;
using System.Reactive.Linq;

using System.Net;
using System.Net.Sockets;

namespace SensorLibrary.Packet.IO
{
    public class EthClient
    {
        public static int PORT = 8000;

        private UdpClient client_;

        public IPAddress Address { get; set; }

        public EthClient()
        {
            this.client_ = new UdpClient(PORT);

        }

        public void Connect()
        {
            this.client_.Connect(new IPEndPoint(this.Address, PORT));
        }

        public IObservable<EthPacket> AsyncReceive()
        {
            var ipend = new IPEndPoint(0, 0);
            return Observable.FromAsyncPattern<byte[]>(this.client_.BeginReceive,
                                                       res => this.client_.EndReceive(res, ref ipend))()
                .Select(a => a.ToObject<EthPacket>());
        }

        public IObservable<Unit> AsyncSend(EthPacket packet)
        {
            var data = packet.ToByteArray();
            return Observable
                .FromAsyncPattern<byte[], int>(this.client_.BeginSend, (res) => this.client_.EndSend(res))(data,
                                                                                                           data.Length);
        }

        public void Send(EthPacket packet)
        {
            var buf = packet.ToByteArray();

            try
            {
                this.client_.Connect(this.Address, PORT);
                this.client_.Send(buf, buf.Length);
            }
            finally
            {
                //this.client_.Close();
            }
        }
    }
}
