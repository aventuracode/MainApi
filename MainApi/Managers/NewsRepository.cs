using AutoMapper;
using MainApi.Managers.Interfaces;
using MainApi.Models;
using MainApi.Models.Dto;
using MainDatabase;
using MainDatabase.dbo.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Net;

namespace MainApi.Repository
{
    public class NewsRepository : INewsRepository
    {
        private IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        protected readonly IConfiguration _configuration;

        private readonly IRedisCacheService _redisCacheService;
        public NewsRepository(IMapper mapper, IHttpClientFactory httpClientFactory, IConfiguration configuration, IRedisCacheService redisCacheService)
        {
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _configuration = configuration;
            _redisCacheService = redisCacheService;
        }
        public async Task<List<ArticleDto>> GetNewsTopHeadlines(string country, int page)
        {
            var URLKEY = $"top-headlines?pageSize=10&country={country}&page={page}&sortBy=popularity"; 
            NewsMapper newsMapper = new NewsMapper();
            var resultCache = await _redisCacheService.Get(URLKEY);
            if (resultCache == null)
            {
                var client = _httpClientFactory.CreateClient("apiNews");
                var response = await client.GetAsync($"{URLKEY}&apiKey={_configuration.GetValue<string>("AppSettings:Token")}");

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                newsMapper = JsonConvert.DeserializeObject<NewsMapper>(responseBody);
                if (newsMapper != null)
                {
                    var result = _mapper.Map<List<ArticleDto>>(newsMapper.articles);
                    await _redisCacheService.Add(URLKEY, JsonConvert.SerializeObject(result));

                    return result;
                }

                return new List<ArticleDto>();
            }
            else
            {
                return JsonConvert.DeserializeObject<List<ArticleDto>>(resultCache);
            }
            
        }

        public async Task<List<ArticleDto>> GetSearchNews(string dateFrom, string dateTo, string keywords, int page)
        {
            var URLKEY = $"everything?q={keywords}&from={dateFrom}&to={dateTo}&page={page}&pageSize=10";

            var client = _httpClientFactory.CreateClient("apiNews");
            
            var response = await client.GetAsync($"{URLKEY}&sortBy=popularity&apiKey={_configuration.GetValue<string>("AppSettings:Token")}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            NewsMapper newsMapper = JsonConvert.DeserializeObject<NewsMapper>(responseBody);
            if (newsMapper != null)
            {
                await _redisCacheService.Add(URLKEY, JsonConvert.SerializeObject(newsMapper.articles));
                return _mapper.Map<List<ArticleDto>>(newsMapper.articles);
            }
            return new List<ArticleDto>();
        }

        
    }
}
