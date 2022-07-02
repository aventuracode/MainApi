using AutoMapper;
using MainApi.Models;
using MainApi.Models.Dto;
using MainDatabase.dbo.Tables;

namespace MainApi.Mapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ArticleDto, Article>();
                config.CreateMap<Article, ArticleDto>();
                config.CreateMap<ArticleDto, Articles>();
                config.CreateMap<Articles, ArticleDto>();
            });
            return mappingConfig;
        }
    }
}
