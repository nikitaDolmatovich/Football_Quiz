using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngleSharp;
using AngleSharp.Attributes;
using AngleSharp.Dom;
using System.Threading.Tasks;
using System.Text;
using AngleSharp.Dom.Html;

namespace Bot.Backend.HelpfulMethodes
{
    public class News
    {
        private const string ADDRESS = "https://by.tribuna.com/football/";

        private async Task<IEnumerable<string>> GetNews()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(ADDRESS);
            var cells = document.GetElementsByClassName("aside-news-list__item");
            var titles = cells.Select(m => m.TextContent).Take(5);

            return titles;
        }

        public async Task<string> ShowNews()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Новости\n");
            var news = await GetNews();

            foreach(var item in news)
            {
                sb.Append("\n" + item + "\n");
            }

            return sb.ToString();
        }
    }
}