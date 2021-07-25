using DAL.Entities;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface INewsRepository
    {
        void AddNews(List<NewsEntity> newsEntity);
        List<NewsEntity> GetNews();
        List<NewsEntity> GetNews(DateTime from, DateTime to);
        List<NewsEntity> GetNews(string text);
    }
}
