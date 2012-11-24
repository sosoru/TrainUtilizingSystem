using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Net;
using Tao.Platform.Windows;

using SensorLibrary;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Control;

using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using DialogConsole;
using RouteLibrary;
using RouteLibrary.Base;

using System.Reactive;
using System.Reactive.Linq;
using Codeplex.Data;

namespace DengoController
{
    class Program
    {
        const string serverAddr = @"http://192.168.2.9:8012/vehicles";

        static IPAddress ipaddr;
        static string RouteName;

        static string GetVehiclesAsString()
        {
            var client = new WebClient();
            return client.DownloadString(serverAddr);
        }

        static void SendCommand(DialogCnosole.VehicleInfoReceived send)
        {
            var client = new WebClient();
            var json = new DataContractJsonSerializer(typeof(DialogCnosole.VehicleInfoReceived));

            using(var ns = client.OpenWrite(serverAddr))
            {
                json.WriteObject(ns, send);
            }
            
        }

        static bool InputVehicles()
        {
            bool result = false;

            var vs = (dynamic[])DynamicJson.Parse(GetVehiclesAsString());

            vs
                .ToObservable()
                .Select((v, i) => string.Format("{0} : {1}", i, v.Name))
                .Subscribe(Console.WriteLine);

            int index;
            if (int.TryParse(Console.ReadLine(), out index))
            {
                if (index >= 0 && index < vs.Count())
                {
                    var v = vs[index];

                    Console.WriteLine("catched vehicle sucessfully");
                    RouteName = v.Name;
                    result = true;
                }
                else
                {
                    Console.WriteLine("out of range index");
                }
            }
            else
            {
                Console.WriteLine("parse error");
            }
            return result;
            
        }

        static void Main(string[] args)
        {
            IDengoController cnt = new DengoController();

            if (!InputVehicles())
                return;

            double infl = 0;
            while (true)
            {
                var ac = cnt.AccelLevel;
                var br = cnt.BrakeLevel;

                if (ac < 0.0 || br < 0.0)
                    continue;

                if (br > 0)
                {
                    infl += -br * 10.0;
                }
                else
                {
                    infl += ac * 5.0;
                }

                if (infl < 0)
                    infl = 0;
                else if (infl > 250)
                    infl = 250;

                //AddAccel(infl, (cnt.Position) ? MotorDirection.Positive : MotorDirection.Negative);
                Console.WriteLine("accel : {0}, brake : {1}, duty : {2},  ",
                            ac * 6, br * 14, infl );

                var data = new DialogCnosole.VehicleInfoReceived()
                {
                    Name = RouteName,
                    Speed = (infl / 250.0f * 100f).ToString(),
                };
                SendCommand(data);

                System.Threading.Thread.Sleep(500);


            }
        }
    }

    class DengoController : IDengoController
    {
        private int sticknumber;

        public DengoController()
        {
            Winmm.JOYINFO info = new Winmm.JOYINFO();

            for (int i = 0; i < Winmm.joyGetNumDevs(); ++i)
            {
                if (Winmm.joyGetPos(i, ref info) == Winmm.JOYERR_NOERROR)
                {
                    this.sticknumber = i;
                    return;
                }
            }

            throw new InvalidOperationException("there is no gamepad");
        }

        private int getkeystate
        {
            get
            {
                Winmm.JOYINFO info = new Winmm.JOYINFO();
                if (Winmm.joyGetPos(this.sticknumber, ref info) == 0)
                {
                    return info.wButtons;
                }

                throw new InvalidOperationException("some error occured when getting keyboard info");
            }
        }

        private int extractbit(int state, int bit)
        {
            return (state & (1 << bit)) >> bit;
        }

        public bool Position
        {
            get
            {
                var state = getkeystate;

                return extractbit(state, 4) > 0;
            }
        }

        public double AccelLevel
        {
            get
            {
                var state = getkeystate;
                var level = extractbit(state, 0) << 0
                            | extractbit(state, 15) << 1
                            | extractbit(state, 13) << 2;

                if (level == 0)
                    return -1.0;

                //invert
                level ^= 7;
                --level;

                return (double)level / 6.0;

            }
        }

        public double BrakeLevel
        {
            get
            {
                var state = getkeystate;
                var level = extractbit(state, 6) << 0
                            | extractbit(state, 4) << 1
                            | extractbit(state, 7) << 2
                            | extractbit(state, 5) << 3;

                if (level == 15)
                    return -1.0;

                //invert
                level ^= 15;
                --level;

                return (double)level / 14.0;

            }
        }
    }
}