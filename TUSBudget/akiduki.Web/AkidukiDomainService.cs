
namespace akiduki.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using System.Net;
    using System.Text.RegularExpressions;

    public class PartsInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public string Comment { get; set; }
        public string PartsImageUri { get; set; }
    }

    public class Page
    {
        public Uri Location { get; set; }
        public string Content { get; set; }
    }

    public abstract class PageParser
    {
        public void FillParameter(PartsInfo info, Page page)
        {
            info.Name = FillName(page);
            info.LongName = FillLongName(page);
            info.Comment = FillName(page);
            info.PartsImageUri = FillPartsImageUri(page);
        }

        public abstract int FillId(Page page);
        public abstract string FillName(Page page);
        public abstract string FillLongName(Page page);
        public abstract string FillComment(Page page);
        public abstract string FillPartsImageUri(Page page);
    }

    public class AkidukiParser : PageParser
    {
        public override int FillId(Page page)
        {
            var reg = new Regex(@"-(\d+?)");
            var m = reg.Match(page.Location.ToString());

            var result = m.Groups [1].Value;
            return int.Parse(result);
        }

        public override string FillName(Page page)
        {
            var reg = new Regex(@"\[(.*?)\]");
            var m = reg.Match(page.Content);

            var result = m.Groups [1].Value;
            return result;
        }

        public override string FillLongName(Page page)
        {
            var reg = new Regex(@"<title>(.+?)</title>");
            var m = reg.Match(page.Content);

            var result = m.Groups [1].Value;
            return result;
        }

        public override string FillComment(Page page)
        {
            return "";
        }

        public override string FillPartsImageUri(Page page)
        {
            var reg = new Regex(@"src=""(/img/goods/L/.*?)""");
            var m = reg.Match(page.Content);

            var result = "http://akizukidenshi.com" + m.Groups [1].Value;

            return result;
        }
    }


    // TODO: アプリケーション ロジックを含むメソッドを作成します。
    public class AkidukiDomainService : DomainService
    {
        [Query(IsDefault=true)]
        public IQueryable<PartsInfo> GetDefault()
        {
            return (new []{ new PartsInfo()}).AsQueryable();
        }

        public IQueryable<PartsInfo> GetInfo(string uri)
        {
            var cli = new WebClient();
            cli.Encoding = System.Text.Encoding.GetEncoding("shift_jis");

            var content = cli.DownloadString(uri);

            var page = new Page() { Location = new Uri(uri), Content = content };
            var data = new PartsInfo();
            var parser = new AkidukiParser();

            parser.FillParameter(data, page);

            return (new [] { data }).AsQueryable();        


        }

    }
}


