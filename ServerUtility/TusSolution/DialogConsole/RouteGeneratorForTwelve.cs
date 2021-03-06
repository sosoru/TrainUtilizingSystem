﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;

using Tus;


namespace DialogConsole
{
    public class RouteGeneratorForTwelve
    {
        private bool Reverse { get; set; }
        private bool UseSubline { get; set; }

        private IEnumerable<string> KwToAb { get; set; }
        private IEnumerable<string> AbSub { get; set; }
        private IEnumerable<string> AbMain { get; set; }
        private IEnumerable<string> AbToKw { get; set; }

        private RouteGeneratorForTwelve() { }

        private IEnumerable<string> GetLoop()
        {
            var list = new List<string>();
            list.AddRange(this.KwToAb);

            if (this.UseSubline)
                list.AddRange(this.AbSub);
            else
                list.AddRange(this.AbMain);

            list.AddRange(this.AbToKw);

            if (this.Reverse)
            {
                var lastisolate = list.Last();
                list.RemoveAt(list.Count - 1);

                list.Reverse();
                list.Add(lastisolate);
            }

            return list;

        }
        public static IEnumerable<string> GetLoopA(bool inv, bool sub)
        {
            var gen = new RouteGeneratorForTwelve()
            {
                Reverse = inv,
                UseSubline = sub,
                KwToAb = new[] { "AT1", "BAT1", },
                AbMain = new[] { "AT3", "AT4", "AT5", "AT6",  },
                AbSub = new[] { "AT3", "AT4S", "AT5S", "AT5-1", "AT6",  },
                AbToKw = new[] { "AT6m", "BAT6", "AT7", "BAT7", "AT8", "BAT8", "AT9", "BAT9", "AT10P", "AT10", "BAT10", "AT11", "BAT11" },
            };

            return gen.GetLoop();
        }

        public static IEnumerable<string> GetLoopB(bool inv, bool sub)
        {
            var gen = new RouteGeneratorForTwelve()
            {
                Reverse = inv,
                UseSubline = sub,
                KwToAb = new[] { "BT1", "BBT1", },
                AbMain = new[] { "BT3", "BT4", "BT5", "BT6", },
                AbSub = new[] { "BT3", "BT4S", "BT5S", "BT5-1", "BT6", },
                AbToKw = new[] { "BT6m","BBT6", "BT7", "BBT7", "BT8", "BBT8", "BT9", "BBT9",  "BT10P", "BT10", "BBT10", "BT11", "BBT11", },
            };
            return gen.GetLoop();
        }

        public static IEnumerable<string> GetLoopC(bool inv, bool sub)
        {
            var gen = new RouteGeneratorForTwelve()
            {
                Reverse = inv,
                UseSubline = sub,
                KwToAb = new[] { "CT1", "BCT1", "CT2-1", "CT2-2", "CT2-3" },
                AbMain = new[] { "CT3", "CT4", "CT5", "CT6", "CT6-1" },
                AbSub = new[] { "CT3", "CT4S", "CT5S", "CT5-1", "CT6", },
                AbToKw = new[] { "BCT6", "CT7", "BCT7", "CT8", "BCT8", "CT9", "BCT9", "CT10", "BCT10", "CT11", "BCT11", },
            };
            return gen.GetLoop();
        }

        public static IEnumerable<string> GetLoopD(bool inv, bool sub)
        {
            var gen = new RouteGeneratorForTwelve()
            {
                Reverse = inv,
                UseSubline = sub,
                KwToAb = new[] { "DT1", "BDT1", "DT2-1", "DT2-2", "DT2-3", "DT2-4", },
                AbMain = new[] { "DT3", "DT4", "DT5", "DT6", "DT6-1" },
                AbSub = new[] { "DT3", "DT4S", "DT5S", "DT5-1", "DT6",  },
                AbToKw = new[] { "BDT6", "DT7", "BDT7", "DT8", "BDT8", "DT9", "BDT9", "DT10", "BDT10", "DT11", "BDT11", },
            };
            return gen.GetLoop();

        }

    }
}
