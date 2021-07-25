using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    [AllowAnonymous]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        /// <summary>
        /// Получение новостей по заданной дате
        /// </summary>
        /// <param name="from">Дата от</param>
        /// <param name="to">Дата до</param>
        /// <returns></returns>
        [HttpGet("posts")]
        public IActionResult GetNews([Required] DateTime from, [Required] DateTime to) =>
            Ok(_newsService.GetNewsService(from,to));

        /// <summary>
        /// Получение топ встречающихся слов
        /// </summary>
        /// <returns></returns>
        [HttpGet("topten")]
        public IActionResult GetNews() =>
            Ok(_newsService.GetNewsService());
        /// <summary>
        /// Получение новостей по искому слову
        /// </summary>
        /// <param name="text">Искомое слово</param>
        /// <returns></returns>
        [HttpGet("search")]
        public IActionResult GetNews([Required] string text) =>
            Ok(_newsService.GetNewsService(text));
    }
}
