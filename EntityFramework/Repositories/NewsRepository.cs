using DAL.Entities;
using DAL.Interfaces;
using EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly DatabaseContext _databaseContext;
        public NewsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public void AddNews(List<NewsEntity> newsEntity)
        {
            if (newsEntity is not null)
            {
                _databaseContext.AddRange(newsEntity);
                _databaseContext.SaveChanges();
            }
        }
        public List<NewsEntity> GetNews() => 
            _databaseContext.News.ToList();
        public List<NewsEntity> GetNews(DateTime from, DateTime to) => 
            _databaseContext.News.Where(x=>x.NewsDate>=from && x.NewsDate<=to).ToList();
        public List<NewsEntity> GetNews(string text) => 
            _databaseContext.News.Where(x => x.Body.Contains(text)).ToList();
    }
}
