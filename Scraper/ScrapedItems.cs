using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scraper
{
    public class ScrapedItems
    {




        internal async void SrapeWebsite(char kitchenFrom, string url)
        {
            try
            {
                CancellationTokenSource cancellationToken = new CancellationTokenSource();
                HttpClient client = new HttpClient();
                HttpResponseMessage request = await client.GetAsync("https://kuchnialidla.pl" + url);
                cancellationToken.Token.ThrowIfCancellationRequested();

                Stream response = await request.Content.ReadAsStreamAsync();
                cancellationToken.Token.ThrowIfCancellationRequested();

                HtmlParser parser = new HtmlParser();
                IHtmlDocument document = parser.ParseDocument(response);
                GetSarperResult(document, kitchenFrom);
            }
            catch (Exception ex)
            {
            }


        }

        private void GetSarperResult(IHtmlDocument document, char kitchenFrom)
        {
            Dish dish = new Dish();
            IEnumerable<IElement> recipeLink;
            var details = document.All.Where(x => x.Id == "details");
            var descriptionText = document.All.Where(x => x.Id == "opis");
            var video = document.All.Where(x => x.ClassName == "video");
            var time = document.All.Where(x => x.ClassName == "meta_time").ToList().Select(x => x.GetElementsByTagName("strong")).ToList().FirstOrDefault().Select(x => x.InnerHtml).FirstOrDefault();
            var ingridientList = document.All.Where(x => x.ClassName == "skladniki").ToList().Select(x => x.InnerHtml).ToList().FirstOrDefault()
                .Replace("\n", "").Replace("<h3>", "").Replace("</h3>", "").Replace("<li>", "").Replace("</li>", "").Replace("<ul>", "")
                .Replace("</ul>", "").Replace("\t", ",").Replace("Składniki", "");
            dish.IngridientsList = ingridientList.Remove(ingridientList.IndexOf(','), 1).Replace("  ", "");
            dish.Name = details.Select(x => x.GetElementsByTagName("h1")).ToList().FirstOrDefault().Select(x => x.InnerHtml).ToList().FirstOrDefault();
            //     description = details.Select(x => x.ClassName == "article").ToList()[0].ToString();
            dish.Description = details.Select(x => x.GetElementsByClassName("article")).ToList().FirstOrDefault().Select(x => x.InnerHtml).ToList().FirstOrDefault();

            var test = descriptionText.Select(x => x).ToList().FirstOrDefault().InnerHtml.Split(new string[] { "</h2>" }, StringSplitOptions.None).ToList();
            test.RemoveRange(0, 1);
            var textH2 = descriptionText.Select(x => x.GetElementsByTagName("h2")).ToList().FirstOrDefault().Select(x => x.InnerHtml).ToList();
            bool toRemove = false;
            if (textH2[0].ToUpper().Contains("PRZYGOTUJ"))
            {
                textH2.RemoveAt(0);
                toRemove = true;
            }
            if (toRemove) test.RemoveAt(0);
            for (int i = 0; i < test.Count; i++)
            {
                //var testList = test[i].Split(new string[] { "<p style" }, StringSplitOptions.None).ToList();

                if (test[i].Contains("<ul>"))
                {
                    test.RemoveAt(i);
                }
                else
                {
                    test[i] = test[i].Substring(0, test[i].LastIndexOf("</p>") == -1 ? 0 : test[i].LastIndexOf("</p>")).Replace("<p style=\"text-align:justify\">", "").Replace("</p>", "").Replace("\n", "");
                }
            }

            dish.Directions1 = "**" + textH2[0] + "**\n\n" + test[0];
            dish.Directions2 = "**" + textH2[1] + "**\n\n" + test[1];
            dish.Directions3 = "**" + textH2[2] + "**\n\n" + test[2];
            dish.Directions4 = "**" + textH2[3] + "**\n\n" + test[3];
            dish.PictUrl = video.Select(x => x.GetElementsByTagName("img")).ToList().FirstOrDefault().Select(x => x).ToList().Select(x => x.GetAttribute("src")).ToList().FirstOrDefault();
            dish.KitchenFrom = kitchenFrom.ToString();
            dish.TimeForPrepare = TimeSpan.Parse(time).Hours * 60 + TimeSpan.Parse(time).Minutes;


            PostClass postClass = new PostClass();
            postClass.PostAsync(dish);

        }
    }
}
