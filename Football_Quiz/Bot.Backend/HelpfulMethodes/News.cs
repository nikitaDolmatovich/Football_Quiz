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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Bot.Backend.HelpfulMethodes
{
    public interface INews
    {
        INews Clone();
    }

    [Serializable]
    public class News : INews
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

        public object DeepCopy()
        {
            object figure = null;
            using (MemoryStream tempStream = new MemoryStream())
            {
                BinaryFormatter binFormatter = new BinaryFormatter(null,
                    new StreamingContext(StreamingContextStates.Clone));

                binFormatter.Serialize(tempStream, this);
                tempStream.Seek(0, SeekOrigin.Begin);

                figure = binFormatter.Deserialize(tempStream);
            }
            return figure;
        }

        public INews Clone()
        {
            return this.MemberwiseClone() as INews;
        }
    }
}