using System;
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

namespace DengoController
{
    class Program
    {
        static DeviceID targetdeviceid_g;
        static PacketServer serv_g;
        static PacketDispatcher dispat_g;
        static Motor mtr_g;

        static void InitCommunication(IPAddress ipbase, IPAddress ipmask)
        {
            serv_g = new PacketServer(new AvrDeviceFactoryProvider());

            var io = new SensorLibrary.Packet.IO.TusEthernetIO(ipbase, ipmask)
                         {
                             SourceID = new DeviceID(10, 0, 0),
                             Port = 8000,
                         };

            serv_g.Controller = io;

            dispat_g = new PacketDispatcher();
            serv_g.AddAction(dispat_g);
            serv_g.LoopStart();
        }

        static DeviceID InformDeviceID()
        {
            while (true)
            {
                Console.WriteLine("type device id : (%d, %d, %d)");
                try
                {
                    var readed = Console.ReadLine();
                    var id = new RouteLibrary.Parser.DeviceIdParser().FromString(readed);

                    return id.First();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void AddAccel(double acc, MotorDirection dir)
        {
            if (mtr_g == null)
                return;

            var state = mtr_g.CurrentState;
            state.Duty = (float)acc / 255.0F;
            state.Direction = MotorDirection.Positive;
            state.ControlMode = MotorControlMode.DutySpecifiedMode;

            mtr_g.SendState();
        }

        static void Main(string[] args)
        {
            IDengoController cnt = new DengoController(); 

            InitCommunication(new IPAddress(new byte[] { 255, 255, 255, 0 }), new IPAddress(new byte[] { 192, 168, 2, 10 }));

            mtr_g = new Motor(serv_g) { DeviceID = InformDeviceID() };
            mtr_g.Observe(dispat_g);

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

                AddAccel(infl, (cnt.Position) ? MotorDirection.Positive : MotorDirection.Negative) ;
                Console.WriteLine("accel : {0}, brake : {1}, duty : {2}, current : {3}, button : {4}",
                            ac * 6, br * 14, infl, mtr_g.CurrentState.Current  );

                System.Threading.Thread.Sleep(200);


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