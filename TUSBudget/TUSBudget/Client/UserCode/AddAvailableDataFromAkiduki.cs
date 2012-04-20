using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Threading;
using System.Net;
using System.Text.RegularExpressions;

namespace LightSwitchApplication
{
    public partial class AddAvailableDataFromAkiduki
    {
        class parsingdata
        {
            public string url { get; set; }
            public string content { get; set; }
        }

        class AvailablePartsParserProvider
        {
            
            public Func<parsingdata,string> NameProv;
            public Func<parsingdata, string> LongNameProv;
            public Func<parsingdata, string> CommentProv;
            public Func<parsingdata, string> PartsImageUriProv;

            public void FillParameter(parsingdata content, AvailableParts parts)
            {
                parts.Name = NameProv(content);
                parts.LongName = LongNameProv(content);
                parts.Comment = CommentProv(content);
                parts.PartsImageUri = PartsImageUriProv(content);
            }
        }

        partial void AvailableParts_Loaded(bool succeeded)
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.AvailableParts);
        }

        partial void AvailableParts_Changed()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.AvailableParts);
        }

        partial void AddAvailableDataFromAkiduki_Saved()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.AvailableParts);
        }

        partial void Parse_AkidukiMethod_Execute()
        {
            // Write your code here.
            var cli = new WebClient();

            var userstate = new { uri = this.TargetUrl };

            cli.DownloadStringCompleted += new DownloadStringCompletedEventHandler(cli_DownloadStringCompleted);
            cli.DownloadStringAsync(new Uri(this.TargetUrl), userstate);
        }

        AvailablePartsParserProvider aki_parser
        {
            get
            {
                var parser = new AvailablePartsParserProvider()
                {
                    NameProv = Aki_NameProv,
                    LongNameProv = Aki_LongNameProv,
                    CommentProv = Aki_CommentProv,
                    PartsImageUriProv = Aki_PartsImageUriProv,
                };
                return parser;
            }
        }

        void cli_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
                return;

            Dispatchers.Main.BeginInvoke(() =>
            {

                this.DataWorkspace.Details.Dispatcher.BeginInvoke(() =>
                {
                    var userstate = (dynamic)e.UserState;
                    var content = e.Result;
                    var entity = this.AvailableParts;
                    var parser = aki_parser;

                    var data = new parsingdata()
                    {
                        url = userstate.uri,
                        content = content,
                    };

                    parser.FillParameter(data, entity);
                });
            });
        }

        string Aki_NameProv(parsingdata content)
        {
            var reg = new Regex(@"\[(.*?)\]");
            var m = reg.Match(content.content);

            var result = m.Groups [1].Value;
            return result;
        }

        string Aki_LongNameProv(parsingdata content)
        {
            var reg = new Regex(@"<title>(.+?)</title>");
            var m = reg.Match(content.content);

            var result = m.Groups [1].Value;
            return result;
        }

        string Aki_CommentProv(parsingdata content)
        {
            return "";
        }

        string Aki_PartsImageUriProv(parsingdata content)
        {
            var reg = new Regex(@"src=""(/img/goods/L/.*?)""");
            var m = reg.Match(content.content);

            var result = "http://akizukidenshi.com/" + m.Groups [1].Value;

            return result;
        }

    }
}