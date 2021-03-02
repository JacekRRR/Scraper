using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scraper
{
    public class links
    {
        internal async void SrapeWebsite(char kitchenFrom, string url)
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            HttpClient client = new HttpClient();
            HttpResponseMessage request = await client.GetAsync(url);
            cancellationToken.Token.ThrowIfCancellationRequested();

            Stream response = await request.Content.ReadAsStreamAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();

            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);

            //GetSarperResult(document, kitchenFrom);

            //HtmlWeb hw = new HtmlWeb();
            //HtmlDocument doc = hw.Load(url);
            //List<string> links = new List<string>();

            //foreach (HtmlNode item in doc..DocumentNode.SelectNodes(("//a[@href]")))
            //{

            //    links.Add(item.InnerText);
            //}
            GetLinks(document,kitchenFrom);
        }

        private void GetLinks(IHtmlDocument document, char kitchenFrom)
        {
            var linkitems = document.All.Where(x => x.Id == "products").ToList();
            var hrefLinks = linkitems.Select(x => x.GetElementsByTagName("a").Select(y => y.GetAttribute("href"))).ToList().FirstOrDefault().Distinct().ToList();
            
            foreach (var item in hrefLinks)
            {
                ScrapedItems scr = new ScrapedItems();
                scr.SrapeWebsite(kitchenFrom, item);
            }
        }



    }
}
