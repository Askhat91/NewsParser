using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ParseNewsController : ControllerBase
    {
        private readonly IParseService _parseService;
        public ParseNewsController(IParseService parseService)
        {
            _parseService = parseService;
        }
        
        [HttpGet("parsenews")]
        public async Task<IActionResult> ParseNewsAsync() => 
            Ok(await _parseService.ParseServicingAsync());
    }
}
