using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tus.Communication;

namespace Tus.Route.Parser
{
    public class DeviceIdParser
    {
        public IEnumerable<DeviceID> FromString(string context)
        {
            if (context.TrimEnd().Last() != ';')
                context += ';';

            var reg = new System.Text.RegularExpressions.Regex(@"\(\s*(\d+)\s*,\s*(\d+)\s*,\s*(\d+)\s*\)\s*;");

            var mates = reg.EnumerateMatches(context);
            foreach (var mat in mates) 
            {
                var id = new DeviceID();
                id.ParentPart = byte.Parse(mat.Groups[1].Value);
                id.ModuleAddr = byte.Parse(mat.Groups[2].Value);
                id.InternalAddr = byte.Parse(mat.Groups[3].Value);
                
                yield return id;
            }
        }
    }
}
