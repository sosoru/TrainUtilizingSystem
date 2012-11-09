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

        // private UdpClient client_;

        public IPAddress Address { get; set; }

        public EthClient()
        {
           // this.client_ = new UdpClient(PORT);

        }

        public void Connect()
        {
            //this.client_.Connect(new IPEndPoint(this.Address, PORT));
        }

        public IObservable<EthPacket> AsyncReceive()
        {
            IPEndPoint ipend;
            var client = new UdpClient(PORT);

            return Observable.FromAsyncPattern<byte[]>(client.BeginReceive,
                                                       res => client.EndReceive(res, ref ipend))()
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
                                            var client = new UdpClient();
                                            byte[] address = new byte[4];

                                            Array.Copy(this.Address.GetAddressBytes(), address, 4);
                                            address[3] = packet.destId.InternalAddr;

                                            client.Connect(new IPEndPoint(address, PORT));

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
                var data = packet.ToByteArray();
                var client = new UdpClient();

                byte[] address = new byte[4];
                Array.Copy(this.Address.GetAddressBytes(), address, 4);
                address[3] = packet.destId.InternalAddr;

                client.Connect(address, PORT);
                client.Send(buf, buf.Length);
            }
            finally
            {
                //this.client_.Close();
            }
        }
    }
}
