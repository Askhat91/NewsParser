using BLL.DTO;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class NewsService: INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly int Count;
        public NewsService(INewsRepository newsRepository, IConfiguration configuration)
        {
            _newsRepository = newsRepository;
            this.Count = Convert.ToInt32(configuration["Count"]);
        }
        public List<TopWords> GetNewsService()
        {
            var AllNews = AutoMapperHelper.ToBusinessLayer(_newsRepository.GetNews());
            string text = string.Empty;
            foreach (var x in AllNews)
                text += x.Body;

            text=Regex.Replace(text, @"[^0-9а-яёА-ЯЁ]+", " ");
            List<TopWords> topWords =text.Split(' ')
            .Where(s => !string.IsNullOrEmpty(s))
            .GroupBy(s => s)
            .OrderByDescending(g => g.Count())
            .Take(Count)
            .Select(x => new TopWords(x.Key.ToString(), x.Count()))
            .ToList();

            return topWords;
        }
        public List<News> GetNewsService(DateTime from, DateTime to)=>
            AutoMapperHelper.ToPresentationLayerMulti(_newsRepository.GetNews(from, to));
        public List<News> GetNewsService(string text) =>
            AutoMapperHelper.ToPresentationLayerMulti(_newsRepository.GetNews(text));
    }
}
