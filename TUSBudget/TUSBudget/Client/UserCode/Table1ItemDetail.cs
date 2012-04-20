using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;

namespace LightSwitchApplication
{
    public partial class Table1ItemDetail
    {
        partial void Table1Item_Loaded(bool succeeded)
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.Table1Item);
        }

        partial void Table1Item_Changed()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.Table1Item);
        }

        partial void Table1ItemDetail_Saved()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.Table1Item);
        }

        partial void Method_Execute()
        {
            // Write your code here.

            
        }


        public IQueryable<EagleBomParts> GetAllNeededPartsOnDir(string path)
        {
            var dir = new DirectoryInfo(path);
            var files = dir.GetFiles("*.htm");
            var qlist = new List<IQueryable<EagleBomParts>>();

            foreach (var f in files)
            {
                using (var fs = f.OpenRead())
                using (var sr = new StreamReader(fs))
                {
                    var cnt = sr.ReadToEnd();
                    var r = new Regex(@"<tr><td>(\d+?)</td><td>(.+?)</td><td>(.+?)</td><td>(.+?)</td></tr>");
                    var mates = r.Matches(cnt);

                    var mlist = new List<EagleBomParts>();
                    var menum = mates.GetEnumerator();

                    while (menum.MoveNext())
                    {
                        var m = menum.Current as Match;

                        var parts = new EagleBomParts()
                        {
                            Board = f.Name,
                            Qty = int.Parse(m.Groups [1].Value),
                            Value = m.Groups [2].Value,
                            Device = m.Groups [3].Value,
                            Parts = m.Groups [4].Value,
                        };
                        parts.Id = gen_id(parts);

                        mlist.Add(parts);
                    }

                    qlist.Add(mlist.AsQueryable());

                }

            }
            return qlist.SelectMany(q => q).AsQueryable();

        }

        [Query(IsDefault = true)]
        public IQueryable<EagleBomParts> GetAllNeededParts()
        {
            var path = @"C:\Users\Administrator\Desktop\rail"; //HttpContext.Current.Server.MapPath("~");

            return this.GetAllNeededPartsOnDir(path);
        }

        private string gen_id(EagleBomParts data)
        {
            var id = data.Board + data.Device + data.Value;

            var md5prov = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var hash = md5prov.ComputeHash(System.Text.UnicodeEncoding.Unicode.GetBytes(id));

            return hash.Select(b => string.Format("{0:00}", b)).Aggregate((agg, n) => agg + n);

        }

    }
}