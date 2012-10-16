using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RouteLibrary.Parser
{
    static class RegexExtention
    {
        public static IEnumerable<Match> EnumerateMatches(this Regex reg, string text)
        {
            var mates = reg.Matches(text);
            var e = mates.GetEnumerator();

            while (e.MoveNext())
            {
                var obj = (Match)e.Current;

                yield return obj;
            }
        }
    }
}
