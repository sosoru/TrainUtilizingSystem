﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RouteLibrary.Base;
using System.Text.RegularExpressions;
namespace RouteLibrary.Parser
{
    public class RouteParser
    {
        public IList<BlockInfo> ReferencedBlocks { get; set; }

        private IEnumerable<Route> parse_route(IEnumerable<string> from, string dir, IEnumerable<string> to)
        {
            var b_from = from.Select(s => ReferencedBlocks.First(b => b.Name == s.Trim()));
            var b_to = to.Select(s => ReferencedBlocks.First(b => b.Name == s.Trim()));

            var pairs_ltor = b_from.SelectMany(f => b_to.Select(t => new Route() { From = f, To = t }));
            var pairs_rtol = b_from.SelectMany(f => b_to.Select(t => new Route() { From = t, To = f }));

            var e = Enumerable.Empty<Route>();

            if (dir.Contains('>'))
                e = e.Concat(pairs_ltor);

            if (dir.Contains('<'))
                e = e.Concat(pairs_rtol);

            return e;            
        }


        private IEnumerable<Route> spilt_route(string context)
        {
            var reg = new Regex(@"(.+?)([\<\>]+)(.+)");

            var mat = reg.Match(context);

            return parse_route(mat.Groups[1].Value.Split(','), mat.Groups[2].Value, mat.Groups[3].Value.Split(','));
        }

        public IEnumerable<Route> FromString(string context)
        {
            var q = context.Split(';')
                           .SelectMany(s => spilt_route(s));

            return q;
        }
    }
}