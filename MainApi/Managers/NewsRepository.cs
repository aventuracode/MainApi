using AutoMapper;
using MainApi.Managers.Interfaces;
using MainApi.Models;
using MainApi.Models.Dto;
using MainDatabase;
using MainDatabase.dbo.Tables;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace MainApi.Repository
{
    public class NewsRepository : INewsRepository
    {
        private IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        protected readonly IConfiguration _configuration;
        public NewsRepository(IMapper mapper, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _configuration = configuration;
        }
        public async Task<List<ArticleDto>> GetNewsTopHeadlines(string pais, int page)
        {
            var client = _httpClientFactory.CreateClient("NewsApi");
            
            var response = await client.GetAsync($"top-headlines?country={pais}&apiKey={_configuration.GetValue<string>("AppSettings:Token")}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            NewsMapper newsMapper = JsonConvert.DeserializeObject<NewsMapper>(responseBody);
            if (newsMapper != null)
                return _mapper.Map<List<ArticleDto>>(newsMapper.articles);

            return new List<ArticleDto>();
        }

        public async Task<List<ArticleDto>> GetSearchNews(string dateFrom, string dateTo, string keywords, int page)
        {
            var client = _httpClientFactory.CreateClient("NewsApi");
            
            var response = await client.GetAsync($"everything?q={keywords}&from={dateFrom}&sortBy=popularity&apiKey={_configuration.GetValue<string>("AppSettings:Token")}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            NewsMapper newsMapper = JsonConvert.DeserializeObject<NewsMapper>(responseBody);
            if (newsMapper != null)
                return _mapper.Map<List<ArticleDto>>(newsMapper.articles);

            return new List<ArticleDto>();
        }

        
    }
}
