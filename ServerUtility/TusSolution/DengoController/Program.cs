using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading;
using Codeplex.Data;
using DialogConsole.Features;
using Tao.Platform.Windows;
using System.Reactive.Linq;

namespace DengoController
{
    internal class Program
    {
        private const string serverAddr = @"http://192.168.2.9:8012/vehicles";

        private static IPAddress ipaddr;
        private static string RouteName;

        private static string GetVehiclesAsString()
        {
            var client = new WebClient();
            return client.DownloadString(serverAddr);
        }

        private static void SendCommand(VehicleInfoReceived send)
        {
            var client = new WebClient();
            var json = new DataContractJsonSerializer(typeof(VehicleInfoReceived));

            using (Stream ns = client.OpenWrite(serverAddr))
            {
                json.WriteObject(ns, send);
            }
        }

        private static bool InputVehicles()
        {
            bool result = false;

            var vs = (dynamic[])DynamicJson.Parse(GetVehiclesAsString());

            foreach (dynamic str in vs
                .Select((v, i) => string.Format("{0} : {1}", i, v.Name)))
            {
                Console.WriteLine(str);
            }


            int index;
            if (int.TryParse(Console.ReadLine(), out index))
            {
                if (index >= 0 && index < vs.Count())
                {
                    dynamic v = vs[index];

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

        private static IDengoController cnt = new DengoController();
        private static double infl = 0;
        private static double before_infl = infl + 1;
        private static double spdmax = 250;
        private static double spdmin = 0;
        private static double accel = 5.0;
        private static double brake = 10.0;
        private static void sendingloop()
        {
            double ac = cnt.AccelLevel;
            double br = cnt.BrakeLevel;

            if (ac < 0.0 || br < 0.0)
                return;

            if (br > 0)
            {
                infl += -br * brake;
            }
            else
            {
                infl += ac * accel;
            }

            if (infl < spdmin)
                infl = spdmin;
            else if (infl > spdmax)
                infl = spdmax;

            //AddAccel(infl, (cnt.Position) ? MotorDirection.Positive : MotorDirection.Negative);
            if (infl != before_infl)
            {
                Console.WriteLine("accel : {0}, brake : {1}, duty : {2},  ",
                                  ac * 6, br * 14, infl);
                var data = new VehicleInfoReceived
                               {
                                   Name = RouteName,
                                   Speed = (infl / 250.0f).ToString(),
                                   Accelation = "1.0"
                               };
                SendCommand(data);
            }
            before_infl = infl;



        }

        private static void Main(string[] args)
        {
            if (!InputVehicles())
                return;

            IDisposable sending = null;
            while (true)
            {
                Console.WriteLine("cmd?");
                var cmd = Console.ReadLine();

                if (cmd.Contains("loop start"))
                {
                    if (sending == null)
                        sending = Observable.Defer(() =>Observable.Start(sendingloop))
                            .Delay(TimeSpan.FromMilliseconds(500))
                            .Repeat()
                            .Subscribe();
                }
                else if (cmd.Contains("loop stop"))
                {
                    if (sending != null)
                    {
                        sending.Dispose();
                        sending = null;
                    }
                }
                else if (cmd.Contains("max"))
                {
                    try
                    {
                        var res = double.TryParse(cmd.Split(' ').Last(), out spdmax);
                        Console.WriteLine("spdmax = {0}", spdmax);
                    }
                    catch 
                    {
                    }
                }
                else if (cmd.Contains("min"))
                {
                    try
                    {
                        var res = double.TryParse(cmd.Split(' ').Last(), out spdmin);
                        Console.WriteLine("spdmin = {0}", spdmin);
                    }catch
                    {
                    }
                }
                 else if (cmd.Contains("accel"))
                {
                    try
                    {
                        var res = double.TryParse(cmd.Split(' ').Last(), out accel);
                        Console.WriteLine("accel = {0}", accel);
                    }
                    catch
                    {
                    }
                }
               else if (cmd.Contains("brake"))
                {
                    try
                    {
                        var res = double.TryParse(cmd.Split(' ').Last(), out brake);
                        Console.WriteLine("brake = {0}", brake);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }

    internal class DengoController : IDengoController
    {
        private readonly int sticknumber;

        public DengoController()
        {
            var info = new Winmm.JOYINFO();

            for (int i = 0; i < Winmm.joyGetNumDevs(); ++i)
            {
                if (Winmm.joyGetPos(i, ref info) == Winmm.JOYERR_NOERROR)
                {
                    sticknumber = i;
                    return;
                }
            }

            throw new InvalidOperationException("there is no gamepad");
        }

        private int getkeystate
        {
            get
            {
                var info = new Winmm.JOYINFO();
                if (Winmm.joyGetPos(sticknumber, ref info) == 0)
                {
                    return info.wButtons;
                }

                throw new InvalidOperationException("some error occured when getting keyboard info");
            }
        }

        public bool Position
        {
            get
            {
                int state = getkeystate;

                return extractbit(state, 4) > 0;
            }
        }

        private double _before_accel =0 ;
        public double AccelLevel
        {
            get
            {
                int state = getkeystate;
                int level = extractbit(state, 0) << 0
                            | extractbit(state, 15) << 1
                            | extractbit(state, 13) << 2;

                if (level == 0)
                    return _before_accel;

                //invert
                level ^= 7;
                --level;

                _before_accel = level / 6.0;
                return _before_accel;
            }
        }

        private double _before_brake = 0;
        public double BrakeLevel
        {
            get
            {
                int state = getkeystate;
                int level = extractbit(state, 6) << 0
                            | extractbit(state, 4) << 1
                            | extractbit(state, 7) << 2
                            | extractbit(state, 5) << 3;

                if (level == 15)
                    return _before_brake;

                //invert
                level ^= 15;
                --level;

                if (level == 14)
                    return double.MaxValue;

                _before_brake = level / 14.0;
                return _before_brake;
            }
        }

        private int extractbit(int state, int bit)
        {
            return (state & (1 << bit)) >> bit;
        }
    }
}