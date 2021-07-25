using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using Models;
using System.Collections.Generic;

namespace BLL.Helpers
{
    public static class AutoMapperHelper
    {
        public static List<NewsEntity> ToDataLayer(List<NewsDTO> NewsList)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NewsDTO, NewsEntity>());
            var mapper = new Mapper(config);
            return mapper.Map<List<NewsDTO>, List<NewsEntity>>(NewsList);
        }
        public static List<NewsDTO> ToBusinessLayer(List<NewsEntity> NewsList)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NewsEntity, NewsDTO> ());
            var mapper = new Mapper(config);
            return mapper.Map<List<NewsEntity>, List<NewsDTO>>(NewsList);
        }
        public static List<News> ToPresentationLayer(List<NewsDTO> NewsList)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NewsDTO,News>());
            var mapper = new Mapper(config);
            return mapper.Map<List<NewsDTO>, List<News>>(NewsList);
        }
        public static List<News> ToPresentationLayerMulti(List<NewsEntity> NewsList)
        {
            List<NewsDTO> NewsListDto = ToBusinessLayer(NewsList);
            return ToPresentationLayer(NewsListDto);
        }
    }
}
