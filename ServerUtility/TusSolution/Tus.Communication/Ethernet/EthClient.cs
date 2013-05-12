using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Threading;

using System.Net;
using System.Net.Sockets;

namespace Tus.Communication.Ethernet
{
    public class EthClient
    {
        public static int SEND_PORT = 8000;
        public static int RECV_PORT = 8001;

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
            try
            {
                IPEndPoint ipend = new IPEndPoint(IPAddress.Any, RECV_PORT);
                var client = new UdpClient(RECV_PORT);

            return Observable.FromAsyncPattern<byte[]>(client.BeginReceive,
                                                       res => client.EndReceive(res, ref ipend))()
                                                       .Do(data => client.Close())
                .Select(a => a.ToObject<EthPacket>());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Observable.Empty<EthPacket>();
            }
        }

        public IPEndPoint ApplyDestID(EthPacket packet)
        {
            byte[] address = new byte[4];

            Array.Copy(this.Address.GetAddressBytes(), address, address.Length);
            address[3] = (byte)packet.destId.ParentPart;

            return new IPEndPoint(new IPAddress(address), SEND_PORT);
        }


        public virtual IObservable<Unit> AsyncSend(EthPacket packet)
        {
            return Observable.Start(() =>
                                        {
                                            var data = packet.ToByteArray();
                                            var client = new UdpClient();
                                            try
                                            {
                                                client.Connect(ApplyDestID(packet));

                                                Observable
                                                    .FromAsyncPattern<byte[], int>(client.BeginSend,
                                                                                   (res) => client.EndSend(res))(data,
                                                                                                                 data.Length);
                                            }
                                            catch (SocketException ex) { }

                                        });
        }

        public void Send(EthPacket packet)
        {
            var buf = packet.ToByteArray();
            var client = new UdpClient();

            try
            {
                var data = packet.ToByteArray();
                client.Connect(ApplyDestID(packet));
                client.Send(buf, buf.Length);
            }
            finally
            {
                //this.client_.Close();
            }
        }
    }
}
