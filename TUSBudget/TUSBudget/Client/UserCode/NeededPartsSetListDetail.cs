using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;

using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace LightSwitchApplication
{
    public partial class NeededPartsSetListDetail
    {
        partial void InsertEagleBoms_Execute()
        {
            // Write your code here.
            Microsoft.LightSwitch.Threading.Dispatchers.Main.BeginInvoke(() =>
            {
                var dialog = new OpenFileDialog();
                dialog.Multiselect = true;

                if (!dialog.ShowDialog().Value)
                    return;

                var files = dialog.Files;
                this.DataWorkspace.Details.Dispatcher.BeginInvoke(() =>
                {
                    foreach (var file in files)
                    {
                        var newentities = new List<NeededParts>();

                        //delete all entity that contained this file before before insert
                        foreach (var entity in this.DataWorkspace.ApplicationData.NeededPartsSet.Search(file.Name).Execute())
                        {
                            if (entity.Board.Contains(file.Name))
                                newentities.Add(entity);
                        }

                        using (var fs = file.OpenRead())
                        using (var sr = new StreamReader(fs))
                        {
                            var content = sr.ReadToEnd();
                            var rx = new Regex(@"<tr><td>(\d*?)</td><td>(.*?)</td><td>(.*?)</td><td>(.*?)</td></tr>");
                            var matches = rx.Matches(content);

                            var enumer = matches.GetEnumerator();
                            while (enumer.MoveNext())
                            {
                                var m = (Match)enumer.Current;

                                var board = file.Name;
                                var qty = int.Parse(m.Groups[1].Value);
                                var value = m.Groups[2].Value;
                                var device = m.Groups[3].Value;
                                var parts = m.Groups[4].Value;

                                var entity = newentities.FirstOrDefault(_ => _.Device == device && _.Value == value);

                                if (entity == null)
                                {
                                    entity = this.DataWorkspace.ApplicationData.NeededPartsSet.AddNew();
                                }
                                else
                                {
                                    newentities.Remove(entity);
                                }

                                entity.Board = board;
                                entity.Qty = qty;
                                entity.Value = value;
                                entity.Device = device;
                                entity.Parts = parts;
                            }
                        }

                        newentities.ForEach(_ => _.Delete());
                    }

                    this.DataWorkspace.ApplicationData.SaveChanges();
                });
            });
        }

    }
}
