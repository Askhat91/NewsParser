using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("gettoken")]
        public IActionResult GetToken([Required] string login, [Required] string password)
        {
            var identity = _tokenService.GetIdentity(login, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid login or password." });
            }

            return Ok(_tokenService.Token(identity));
        }
    }
}
