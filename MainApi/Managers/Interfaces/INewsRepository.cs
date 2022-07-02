using MainApi.Mapper;
using MainApi.Models.Dto;

namespace MainApi.Managers.Interfaces
{
    public interface INewsRepository
    {
        Task<List<ArticleDto>> GetSearchNews(string dateFrom, string dateTo, string keywords, int page);
        Task<List<ArticleDto>> GetNewsTopHeadlines(string pais, int page);
    }
}
