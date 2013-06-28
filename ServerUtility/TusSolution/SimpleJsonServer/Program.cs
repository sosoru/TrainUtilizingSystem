using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SimpleJsonServer
{
    class Program
    {
        const string jsoncontext = @"[{""AvailableRoutes"":null,""CurrentBlock"":{""Name"":""AT1""},""Halt"":[],""Length"":2,""Name"":""A"",""Route"":{""LockedBlocks"":[{""Name"":""AT12""},{""Name"":""BAT12""},{""Name"":""AT13""},{""Name"":""AT14""},{""Name"":""AT1""},{""Name"":""AT3""},{""Name"":""AT4""},{""Name"":""BAT4""},{""Name"":""AT5""},{""Name"":""BAT5""}],""LockedUnits"":{""_array"":[{""Blocks"":[{""Name"":""AT12""},{""Name"":""BAT12""}]},{""Blocks"":[{""Name"":""AT13""},{""Name"":""AT14""},{""Name"":""AT1""},{""Name"":""AT3""},{""Name"":""AT4""},{""Name"":""BAT4""}]},{""Blocks"":[{""Name"":""AT5""},{""Name"":""BAT5""}]},null],""_head"":0,""_size"":3,""_tail"":3,""_version"":3068},""Name"":""MAIN_REV""},""Speed"":0.68,""VehicleID"":1}]";

        static void Main(string[] args)
        {
            var listener = new HttpListener();
            var prefix = "http://+:8012/";

            listener.Prefixes.Add(prefix);
            listener.Start();

            while (true)
            {
                Console.WriteLine("waiting request...");

                var context = listener.GetContext();
                var req = context.Request;
                var res = context.Response;
                if (req.HttpMethod == "GET")
                {
                    res.Headers.Add("Content-type: application/json");
                    res.Headers.Add("Access-Control-Allow-Headers: x-requested-with, accept");
                    res.Headers.Add("Access-Control-Allow-Origin: *");
                    
                    using (var sw = new StreamWriter(res.OutputStream))
                    {
                        sw.Write(jsoncontext);
                    }
                }
                else
                {
                    Console.WriteLine("{0} method received", req.HttpMethod);
                    using (var sr = new StreamReader(req.InputStream))
                    {
                        Console.WriteLine();
                        Console.WriteLine(sr.ReadToEnd());
                    }
                }
            }

        }
    }
}
