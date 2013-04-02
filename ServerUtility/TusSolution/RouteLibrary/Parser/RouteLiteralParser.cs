using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tus.TransControl.Base;

namespace Tus.TransControl.Parser
{
    public class RouteLiteralParser
    {
        public IList<BlockInfo> ReferencedBlocks { get; set; }

        //戻り値の並びを保証せよ : A>B, A<B
        private IEnumerable<RouteSegmentInfo> parse_route(IEnumerable<string> from, string dir, IEnumerable<string> to)
        {
            var b_from = from.Select(s => ReferencedBlocks.FirstOrDefault(b => b.Name == s.Trim()));
            var b_to = to.Select(s => ReferencedBlocks.FirstOrDefault(b => b.Name == s.Trim()));

            var pairs_ltor = b_from.SelectMany(f => b_to.Select(t => new RouteSegmentInfo() { From = f, To = t }));
            var pairs_rtol = b_from.SelectMany(f => b_to.Select(t => new RouteSegmentInfo() { From = t, To = f }));

            var e = Enumerable.Empty<RouteSegmentInfo>();

            if (dir.Contains('>'))
                e = e.Concat(pairs_ltor);

            if (dir.Contains('<'))
                e = e.Concat(pairs_rtol);

            return e;            
        }


        private IEnumerable<RouteSegmentInfo> spilt_route(string context)
        {
            var reg = new Regex(@"(.+?)([\<\>]+)(.+)");

            var mat = reg.Match(context);

            return parse_route(mat.Groups[1].Value.Split(','), mat.Groups[2].Value, mat.Groups[3].Value.Split(','));
        }

        public IEnumerable<RouteSegmentInfo> FromString(string context)
        {
            var q = context.Split(';')
                           .SelectMany(s => spilt_route(s));

            return q;
        }
    }
}
