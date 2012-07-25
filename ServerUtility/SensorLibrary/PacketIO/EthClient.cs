using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace SensorLibrary.Eth
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
