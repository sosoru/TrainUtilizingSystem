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

        public virtual IObservable<Unit> AsyncSend(EthPacket packet)
        {
            //return Observable
            //    .Using(
            //        () => new UdpClient(),
            //        client =>
            //            {
            //                var data = packet.ToByteArray();
            //                client.Connect(new IPEndPoint(this.Address, PORT));
            //                return
            //                    Observable
            //                    .FromAsyncPattern<byte[], int>(client.BeginSend, (res) => client.EndSend(res))(data, data.Length);

            //            });
            return Observable.Start(() =>
                                        {
                                            var data = packet.ToByteArray();
                                            var client = this.client_;
                                            client.Connect(new IPEndPoint(this.Address, PORT));

                                            Observable
                                                .FromAsyncPattern<byte[], int>(client.BeginSend,
                                                                               (res) => client.EndSend(res))(data,
                                                                                                             data.Length);

                                        });
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
