using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace RailroaderIO
{
    public class RailroaderMap
    {
        public RailroaderMap(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            using (var sr = new StreamReader(fs, System.Text.Encoding.GetEncoding("Shift-JIS")))
            {
                Header = sr.ReadLine() + sr.ReadLine() + sr.ReadLine();

                var rails = new List<RailroaderRailData>();
                while (sr.Peek() >= 0)
                {
                    var line = sr.ReadLine();
                    var rail = new RailroaderRailData(line.Split(',').ToList());

                    rails.Add(rail);
                }

                this.rails_ = new ReadOnlyCollection<RailroaderRailData>(rails);
            }
        }

        public string Header { get; set; }

        public int LayoutHeight
        {
            get
            {
                var reg = new Regex("Width\\s*?:\\s*?(\\d+)");
                var mat = reg.Match(this.Header);

                if (mat.Success && mat.Length >= 1)
                {
                    var h = int.Parse(mat.Groups [1].Value);

                    return h;
                }
                else
                    return 0;

            }
        }

        public int LayoutWidth
        {
            get
            {
                var reg = new Regex("Height\\s*?:\\s*?(\\d+)");
                var mat = reg.Match(this.Header);

                if (mat.Success && mat.Length >= 1)
                {
                    var h  = int.Parse(mat.Groups [1].Value);
                    return h;
                }
                else
                    return 0;
            }
        }


        private ReadOnlyCollection<RailroaderRailData> rails_;
        public ReadOnlyCollection<RailroaderRailData> Rails
        {
            get { return this.rails_; }
        }
    }
}
