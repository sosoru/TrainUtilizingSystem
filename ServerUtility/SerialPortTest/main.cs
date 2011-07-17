using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;

namespace SerialPortTest
{
    static class App
    {
        [STAThread]
        public static void Main()
        {
            var ports = SerialPort.GetPortNames();

            for (int i = 0; i < ports.Length; i++)
            {
                Console.WriteLine("{0}:{1}", i, ports[i]);
            }

            var strnum = Console.ReadKey().KeyChar.ToString();
            int num = -1;
            Console.WriteLine("\r\nOpening...");
            if (int.TryParse(strnum, out num))
            {
                var sport = new SerialPort(ports[num], 9600, Parity.None, 8, StopBits.One);
                sport.Encoding = Encoding.UTF8;
                sport.NewLine = "\n";
                sport.RtsEnable = true;
                sport.DtrEnable = true;
                sport.DiscardNull = false;
                sport.ReadTimeout = 10000;

                int counter = 0;
                while (true)
                {
                    try
                    {
                        if (sport.IsOpen)
                            sport.Close();

                        sport.Open();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(counter);
                        System.Threading.Thread.Sleep(1000);
                        if (++counter > 10)
                            throw;
                    }
                }
                Console.WriteLine("opened successfully");

                var server = new PacketServer(sport.BaseStream);
                var dispatcher = new PacketDispatcher();
                var cens = dispatcher.GetTrainCensor(new DeviceID() { ParentPart = 1, ModulePart = 1 });
                server.AddAction(dispatcher);
                cens.PacketReceived += new PacketReceivedDelegate((dev, args) => Console.WriteLine(dev.ToString()));
                server.LoopStart();

                while (true)
                {
                    System.Threading.Thread.Sleep(100);
                }
                
                //bool sta = true, stab = false;
                //var watch = new System.Diagnostics.Stopwatch();
                //while (sport.IsOpen)
                //{
                //    //char[] buf = new char[64];
                //    //sport.Read(buf, 0, 64);

                //    //var output = new string(buf, 16, 48);
                //    //Console.WriteLine(output);
                //    string context = "";// sport.ReadLine();
                //    byte[] buffer = new byte[256];
                //    sport.Read(buffer, 0, 8);

                //    int startIndex = 0;
                //    int tmp = 0;

                //    while (!int.TryParse(context[startIndex].ToString(), out tmp))
                //        startIndex++;

                //    var val = float.Parse(context.Substring(startIndex, context.Length - startIndex).Trim().Split(',')[0]);
                //    if (!stab && val <= 2.0)
                //    {
                //        Console.WriteLine("{0} detecting", DateTime.Now.ToString());

                //        stab = true;
                //        if (sta) // first
                //        {
                //            watch.Restart();
                //            sta = false;
                //        }
                //        else
                //        {
                //            watch.Stop();
                //            if (watch.Elapsed.TotalSeconds < 5.0)
                //            {
                //                Console.WriteLine("{0}sec. v={1}m/s", watch.Elapsed.TotalSeconds, 0.05 / watch.Elapsed.TotalSeconds);
                //                sta = true;
                //            }
                //            else
                //            {
                //                // modify meisuring position
                //                watch.Restart();
                //                sta = false ;
                //            }
                //        }
                //    }
                //    else if (stab && val > 2.0)
                //    {
                //        stab = false;
                //        Console.WriteLine("{0} detected", DateTime.Now.ToString());

                //    }

                //}
            }

        }
    }
}
