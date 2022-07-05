using MainApi.Managers.Interfaces;
using MainApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _NewsRepository;
        protected Response _response;
        public NewsController(INewsRepository newsRepository)
        {
            _NewsRepository = newsRepository;
            _response = new Response();
        }
        
        [Route("search")]
        [HttpGet]
        public async Task<ActionResult> GetSearchNews(string dateFrom = "2022-07-01", string dateTo = "2022-07-04", string keywords = "dolar", int page = 1)
        {
            try
            {
                var lista = await _NewsRepository.GetSearchNews(dateFrom, dateTo, keywords, page);
                _response.Result = lista;
                _response.Displaymessage = "Resultados de busqueda";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [Route("topheadlines")]
        [HttpGet]
        public async Task<ActionResult> GetNewsTopHeadlines(string country = "ar", int page = 1)
        {
            try
            {
                var lista = await _NewsRepository.GetNewsTopHeadlines(country, page);
                _response.Result = lista;
                _response.Displaymessage = "Titulares más importantes por país";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);

        }
    }
}
