using BLL.DTO;
using Models;
using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface INewsService
    {
        List<TopWords> GetNewsService();
        List<News> GetNewsService(DateTime from, DateTime to);
        List<News> GetNewsService(string text);
    }
}
