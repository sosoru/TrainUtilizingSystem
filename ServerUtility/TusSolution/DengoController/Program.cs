using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Net;
using System.
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
            dispat_g = new PacketDispatcher();

            var io = new SensorLibrary.Packet.IO.TusEthernetIO(ipbase, ipmask)
                         {
                             SourceID = new DeviceID(9, 0, 0),
                             Port = 8000,
                         };

            serv_g.AddAction(dispat_g);
            serv.Controller = io;
            serv.LoopStart();
        }

        static DeviceID InformDeviceID()
        {
            while(true)
            {
                Console.WriteLine("type device id : (%d, %d, %d)");
                try{
                    var readed = Console.ReadLine();
                    var id  = new RouteLibrary.Parser.DeviceIdParser().FromString(readed);

                    return id ;
                }catch(Exception ex){
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void ApplyAccel(double acc)
        {
            if(mtr_g == null)
                return;
            
            mtr_g

        static void Main(string[] args)
        {
            var cnt = new DengoController();
            InitCommunication(new IPAddress(new[] { 255,255,255,0}), new IPAddress(new[]{192,168,2,10}));
            while (true)
            {
                var ac = cnt.AccelLevel;
                var br = cnt.BrakeLevel;

                if (ac < 0.0 || br < 0.0)
                    continue;

                Console.WriteLine("accel : {0}, brake : {1}", ac*6, br*14);

                System.Threading.Thread.Sleep(100);
            }
        }
    }

    class DengoController
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

        public double AccelLevel
        {
            get
            {
                var state = getkeystate;
                var level = extractbit(state, 0) << 0
                            | extractbit(state,15) << 1
                            | extractbit(state,13) << 2;

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