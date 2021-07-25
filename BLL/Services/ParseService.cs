using BLL.DTO;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ParseService: IParseService
    {
        private readonly INewsRepository _newsRepository;
        private readonly string Url, XPath, XpBody, XpTitle, XpTime;
        public ParseService(INewsRepository newsRepository, IConfiguration configuration)
        {
            _newsRepository = newsRepository;
            this.Url = configuration["NewsPath:Url"];
            this.XPath = configuration["NewsPath:XPath"];
            this.XpBody = configuration["NewsPath:XpBody"];
            this.XpTitle = configuration["NewsPath:XpTitle"];
            this.XpTime = configuration["NewsPath:XpDate"];
        }
        /// <summary>
        /// Основоной парсинг
        /// </summary>
        public async Task<List<News>> ParseServicingAsync()
        {
            HtmlDocument doc = new();
            doc = await LoadDocAsync(Url);
            IEnumerable<HtmlNode> nodes = doc.DocumentNode.SelectNodes(XPath).Take(30);
            List<NewsDTO> NewsList = new();
            foreach (var node in nodes)
                NewsList.Add(await GetNewsAsync(GetUri(Url, node?.GetAttributeValue("href", string.Empty))));                
 
            _newsRepository.AddNews(AutoMapperHelper.ToDataLayer(NewsList));
            return AutoMapperHelper.ToPresentationLayer(NewsList);
        }
        /// <summary>
        /// Преоброзование DTO
        /// </summary>
        /// <param name="url">сслыка на страницу</param>
        /// <returns></returns>
        private async Task<NewsDTO> GetNewsAsync(string url)
        {
            HtmlDocument doc = await LoadDocAsync(url);

            NewsDTO model = new()
            {
                Link = url,
                Title = RemoveCharacters(doc.DocumentNode.SelectSingleNode(XpTitle).InnerText),
                Body = RemoveCharacters(doc.DocumentNode.SelectSingleNode(XpBody).InnerText),
                NewsDate = DateTimeHelper.ToDateTime(RemoveCharacters(doc.DocumentNode.SelectSingleNode(XpTime).InnerText))
            };

            return model;
        }
        /// <summary>
        /// Загрузка Html документа
        /// </summary>
        /// <param name="url"> ссылка на скачивания</param>
        /// <returns></returns>
        private async Task<HtmlDocument> LoadDocAsync(string url)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument doc = await htmlWeb.LoadFromWebAsync(url);
            return doc;
        }
        private string RemoveCharacters(string text)=>
            Regex.Replace(text, @"\t|\n|\r", "");
        private string GetUri(string url, string urn) =>
            urn.Contains(url) ? urn : url + urn;
    }
}
